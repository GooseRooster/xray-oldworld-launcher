use std::collections::HashMap;
use std::fs;
use std::path::Path;

use crate::logging;

/// Represents a parsed user.ltx file.
/// user.ltx uses a flat format with no sections: `command value` per line.
pub struct UserLtx {
    /// Ordered entries preserving file structure for write-back.
    entries: Vec<UserLtxEntry>,
    /// Fast lookup: command name -> index in entries vec.
    index: HashMap<String, usize>,
}

#[derive(Debug, Clone)]
enum UserLtxEntry {
    Command { name: String, value: String },
    Comment(String),
    Empty,
}

#[allow(dead_code)]
impl UserLtx {
    pub fn new() -> Self {
        UserLtx {
            entries: Vec::new(),
            index: HashMap::new(),
        }
    }

    /// Load user.ltx from the appdata directory.
    /// Returns empty state if file doesn't exist.
    pub fn load(appdata_path: &Path) -> Self {
        let path = Self::file_path(appdata_path);
        logging::log(format!("Loading user.ltx from: {}", path.display()));

        if !path.exists() {
            logging::log("user.ltx does not exist, returning empty state");
            return Self::new();
        }

        match fs::read_to_string(&path) {
            Ok(content) => {
                let ltx = Self::parse(&content);
                logging::log(format!(
                    "user.ltx loaded: {} bytes, {} commands parsed",
                    content.len(),
                    ltx.index.len()
                ));
                ltx
            }
            Err(e) => {
                logging::log(format!("ERROR reading user.ltx: {}", e));
                Self::new()
            }
        }
    }

    /// Load user.ltx from appdata. If missing, fall back to user_default.ltx
    /// in game root so the launcher shows OWA baseline values instead of empty state.
    pub fn load_with_fallback(appdata_path: &Path, game_root: &Path) -> Self {
        let user_path = Self::file_path(appdata_path);
        if user_path.exists() {
            return Self::load(appdata_path);
        }

        let default_path = game_root.join("user_default.ltx");
        logging::log(format!(
            "user.ltx not found, falling back to user_default.ltx at: {}",
            default_path.display()
        ));

        if !default_path.exists() {
            logging::log("user_default.ltx also not found, returning empty state");
            return Self::new();
        }

        match fs::read_to_string(&default_path) {
            Ok(content) => {
                let ltx = Self::parse(&content);
                logging::log(format!(
                    "OWA: Applied mod defaults from user_default.ltx ({} commands)",
                    ltx.index.len()
                ));
                ltx
            }
            Err(e) => {
                logging::log(format!("ERROR reading user_default.ltx: {}", e));
                Self::new()
            }
        }
    }

    /// Parse user.ltx content.
    fn parse(content: &str) -> Self {
        let mut ltx = UserLtx::new();

        for line in content.lines() {
            let trimmed = line.trim();

            if trimmed.is_empty() {
                ltx.entries.push(UserLtxEntry::Empty);
                continue;
            }

            // Comment lines start with ; or //
            if trimmed.starts_with(';') || trimmed.starts_with("//") {
                ltx.entries.push(UserLtxEntry::Comment(line.to_string()));
                continue;
            }

            // Command-value pair: split on first whitespace
            // Format: `command_name value` or `command_name` (no value)
            let (name, value) = match trimmed.find(char::is_whitespace) {
                Some(pos) => {
                    let name = trimmed[..pos].to_string();
                    let value = trimmed[pos..].trim().to_string();
                    (name, value)
                }
                None => (trimmed.to_string(), String::new()),
            };

            let idx = ltx.entries.len();
            ltx.index.insert(name.clone(), idx);
            ltx.entries.push(UserLtxEntry::Command { name, value });
        }

        ltx
    }

    /// Get the value of a console command.
    pub fn get(&self, command: &str) -> Option<&str> {
        self.index.get(command).and_then(|&idx| {
            if let UserLtxEntry::Command { value, .. } = &self.entries[idx] {
                Some(value.as_str())
            } else {
                None
            }
        })
    }

    /// Get all command-value pairs.
    pub fn get_all(&self) -> HashMap<String, String> {
        let mut result = HashMap::new();
        for entry in &self.entries {
            if let UserLtxEntry::Command { name, value } = entry {
                result.insert(name.clone(), value.clone());
            }
        }
        result
    }

    /// Set a console command value. Adds if not present, updates if exists.
    pub fn set(&mut self, command: &str, value: &str) {
        if let Some(&idx) = self.index.get(command) {
            self.entries[idx] = UserLtxEntry::Command {
                name: command.to_string(),
                value: value.to_string(),
            };
        } else {
            let idx = self.entries.len();
            self.index.insert(command.to_string(), idx);
            self.entries.push(UserLtxEntry::Command {
                name: command.to_string(),
                value: value.to_string(),
            });
        }
    }

    /// Check if a command exists.
    pub fn has(&self, command: &str) -> bool {
        self.index.contains_key(command)
    }

    /// Remove a command entry.
    pub fn remove(&mut self, command: &str) -> bool {
        if let Some(&idx) = self.index.get(command) {
            self.entries[idx] = UserLtxEntry::Empty;
            self.index.remove(command);
            true
        } else {
            false
        }
    }

    /// Save user.ltx to disk, preserving structure.
    pub fn save(&self, appdata_path: &Path) -> Result<(), String> {
        let path = Self::file_path(appdata_path);
        let mut output = String::new();

        for entry in &self.entries {
            match entry {
                UserLtxEntry::Command { name, value } => {
                    if value.is_empty() {
                        output.push_str(name);
                    } else {
                        output.push_str(&format!("{} {}", name, value));
                    }
                    output.push('\n');
                }
                UserLtxEntry::Comment(text) => {
                    output.push_str(text);
                    output.push('\n');
                }
                UserLtxEntry::Empty => {
                    output.push('\n');
                }
            }
        }

        fs::write(&path, output)
            .map_err(|e| format!("Failed to write user.ltx to {:?}: {}", path, e))
    }

    fn file_path(appdata_path: &Path) -> std::path::PathBuf {
        appdata_path.join("user.ltx")
    }
}
