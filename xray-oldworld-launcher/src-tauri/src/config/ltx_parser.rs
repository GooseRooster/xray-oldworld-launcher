use indexmap::IndexMap;
use std::fs;
use std::path::{Path, PathBuf};

/// Represents a parsed LTX file (INI-like format used by S.T.A.L.K.E.R.)
#[derive(Debug, Clone)]
pub struct LtxFile {
    /// Ordered sections: section_name -> ordered key-value pairs
    /// Values are Option<String> to support empty values (key = )
    sections: IndexMap<String, IndexMap<String, Option<String>>>,
    /// Root-level entries (before any section header)
    root: IndexMap<String, Option<String>>,
}

impl LtxFile {
    pub fn new() -> Self {
        LtxFile {
            sections: IndexMap::new(),
            root: IndexMap::new(),
        }
    }

    /// Parse an LTX file, resolving #include directives relative to the file's directory.
    pub fn parse(path: &Path) -> Result<Self, LtxError> {
        let base_dir = path
            .parent()
            .ok_or_else(|| LtxError::Io(format!("Cannot get parent directory of {:?}", path)))?;
        let content = fs::read_to_string(path)
            .map_err(|e| LtxError::Io(format!("Failed to read {:?}: {}", path, e)))?;
        Self::parse_content(&content, base_dir)
    }

    /// Parse LTX content string, resolving includes relative to base_dir.
    fn parse_content(content: &str, base_dir: &Path) -> Result<Self, LtxError> {
        let mut file = LtxFile::new();
        let mut current_section: Option<String> = None;

        for line in content.lines() {
            let trimmed = line.trim();

            // Skip empty lines
            if trimmed.is_empty() {
                continue;
            }

            // Skip comment lines
            if trimmed.starts_with(';') {
                continue;
            }

            // Handle #include directives
            if trimmed.starts_with("#include") {
                let include_path = Self::parse_include_directive(trimmed)?;
                let included = Self::resolve_include(&include_path, base_dir)?;
                file.merge(included);
                continue;
            }

            // Handle section headers
            if trimmed.starts_with('[') {
                if let Some(end) = trimmed.find(']') {
                    let section_name = trimmed[1..end].trim().to_string();
                    file.sections
                        .entry(section_name.clone())
                        .or_insert_with(IndexMap::new);
                    current_section = Some(section_name);
                    continue;
                }
            }

            // Handle key = value pairs
            if let Some(eq_pos) = trimmed.find('=') {
                let key = trimmed[..eq_pos].trim().to_string();
                let raw_value = trimmed[eq_pos + 1..].trim();

                // Strip inline comments (semicolon not inside quotes)
                let value_str = Self::strip_inline_comment(raw_value);
                let value = if value_str.is_empty() {
                    None
                } else {
                    Some(value_str.to_string())
                };

                if let Some(ref section) = current_section {
                    file.sections
                        .entry(section.clone())
                        .or_insert_with(IndexMap::new)
                        .insert(key, value);
                } else {
                    file.root.insert(key, value);
                }
            }
        }

        Ok(file)
    }

    /// Parse an #include directive and extract the filename/pattern.
    fn parse_include_directive(line: &str) -> Result<String, LtxError> {
        // Formats: #include "filename.ltx" or #include "pattern_*.ltx"
        let rest = line.trim_start_matches("#include").trim();
        if rest.starts_with('"') && rest.ends_with('"') && rest.len() >= 2 {
            Ok(rest[1..rest.len() - 1].to_string())
        } else {
            Err(LtxError::Parse(format!(
                "Invalid #include directive: {}",
                line
            )))
        }
    }

    /// Resolve an include path/pattern, supporting wildcards.
    fn resolve_include(pattern: &str, base_dir: &Path) -> Result<LtxFile, LtxError> {
        let full_pattern = base_dir.join(pattern);
        let pattern_str = full_pattern.to_string_lossy().to_string();

        if pattern.contains('*') || pattern.contains('?') {
            // Wildcard include — glob match
            let mut merged = LtxFile::new();
            let entries: Vec<PathBuf> = glob::glob(&pattern_str)
                .map_err(|e| LtxError::Io(format!("Invalid glob pattern '{}': {}", pattern, e)))?
                .filter_map(|entry| entry.ok())
                .collect();

            if entries.is_empty() {
                // Not an error — wildcard may match nothing
                return Ok(merged);
            }

            for entry in entries {
                let included = LtxFile::parse(&entry)?;
                merged.merge(included);
            }
            Ok(merged)
        } else {
            // Direct file include
            let file_path = base_dir.join(pattern);
            if !file_path.exists() {
                return Err(LtxError::Io(format!(
                    "Included file not found: {:?}",
                    file_path
                )));
            }
            LtxFile::parse(&file_path)
        }
    }

    /// Strip inline comments (everything after unquoted `;`)
    fn strip_inline_comment(value: &str) -> &str {
        // Find first `;` that isn't inside quotes
        let mut in_quotes = false;
        for (i, c) in value.char_indices() {
            if c == '"' {
                in_quotes = !in_quotes;
            } else if c == ';' && !in_quotes {
                return value[..i].trim();
            }
        }
        value
    }

    /// Merge another LtxFile into this one (for includes).
    /// Existing sections are extended, not overwritten.
    fn merge(&mut self, other: LtxFile) {
        for (key, value) in other.root {
            self.root.insert(key, value);
        }
        for (section, entries) in other.sections {
            let target = self.sections.entry(section).or_insert_with(IndexMap::new);
            for (key, value) in entries {
                target.insert(key, value);
            }
        }
    }

    // -- Read API --

    /// Get a raw string value from a section.
    pub fn get(&self, section: &str, key: &str) -> Option<&str> {
        self.sections
            .get(section)
            .and_then(|s| s.get(key))
            .and_then(|v| v.as_deref())
    }

    /// Get a value parsed as bool. Handles "true"/"false", "on"/"off", "1"/"0".
    pub fn get_bool(&self, section: &str, key: &str) -> Option<bool> {
        self.get(section, key).and_then(|v| match v.to_lowercase().as_str() {
            "true" | "on" | "1" | "yes" => Some(true),
            "false" | "off" | "0" | "no" => Some(false),
            _ => None,
        })
    }

    /// Get a value parsed as f64.
    pub fn get_float(&self, section: &str, key: &str) -> Option<f64> {
        self.get(section, key).and_then(|v| v.parse::<f64>().ok())
    }

    /// Check if a section exists.
    pub fn has_section(&self, section: &str) -> bool {
        self.sections.contains_key(section)
    }

    /// Check if a key exists in a section (even if value is empty).
    pub fn has_key(&self, section: &str, key: &str) -> bool {
        self.sections
            .get(section)
            .map_or(false, |s| s.contains_key(key))
    }

    /// List all section names.
    pub fn sections(&self) -> Vec<&str> {
        self.sections.keys().map(|s| s.as_str()).collect()
    }

    /// Get all key-value pairs in a section.
    pub fn section_entries(&self, section: &str) -> Option<&IndexMap<String, Option<String>>> {
        self.sections.get(section)
    }

    /// Get all sections and their entries (for serialization).
    pub fn all_entries(&self) -> &IndexMap<String, IndexMap<String, Option<String>>> {
        &self.sections
    }

    // -- Write API --

    /// Set a value in a section. Creates section if needed.
    pub fn set(&mut self, section: &str, key: &str, value: Option<&str>) {
        self.sections
            .entry(section.to_string())
            .or_insert_with(IndexMap::new)
            .insert(key.to_string(), value.map(|v| v.to_string()));
    }

    /// Remove a key from a section.
    pub fn remove(&mut self, section: &str, key: &str) -> bool {
        self.sections
            .get_mut(section)
            .map_or(false, |s| s.shift_remove(key).is_some())
    }

    /// Write the LTX file to disk.
    /// Preserves section ordering. Uses consistent formatting.
    pub fn write(&self, path: &Path) -> Result<(), LtxError> {
        let content = self.to_string_formatted();
        fs::write(path, content)
            .map_err(|e| LtxError::Io(format!("Failed to write {:?}: {}", path, e)))
    }

    /// Format the LTX file as a string with aligned key-value pairs.
    fn to_string_formatted(&self) -> String {
        let mut output = String::new();

        // Root entries (rare, but handle them)
        for (key, value) in &self.root {
            match value {
                Some(v) => output.push_str(&format!("{} = {}\n", key, v)),
                None => output.push_str(&format!("{} =\n", key)),
            }
        }
        if !self.root.is_empty() {
            output.push('\n');
        }

        // Sections
        for (i, (section_name, entries)) in self.sections.iter().enumerate() {
            if i > 0 || !self.root.is_empty() {
                output.push('\n');
            }
            output.push_str(&format!("[{}]\n", section_name));

            if entries.is_empty() {
                continue;
            }

            // Calculate max key length for alignment padding
            let max_key_len = entries.keys().map(|k| k.len()).max().unwrap_or(0);

            for (key, value) in entries {
                let padded_key = format!("{:width$}", key, width = max_key_len);
                match value {
                    Some(v) => output.push_str(&format!("        {} = {}\n", padded_key, v)),
                    None => output.push_str(&format!("        {} =\n", padded_key)),
                }
            }
        }

        output
    }
}

#[derive(Debug)]
pub enum LtxError {
    Io(String),
    Parse(String),
}

impl std::fmt::Display for LtxError {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        match self {
            LtxError::Io(msg) => write!(f, "IO error: {}", msg),
            LtxError::Parse(msg) => write!(f, "Parse error: {}", msg),
        }
    }
}

impl std::error::Error for LtxError {}
