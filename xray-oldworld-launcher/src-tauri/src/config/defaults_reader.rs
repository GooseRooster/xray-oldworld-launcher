use std::collections::HashMap;
use std::path::{Path, PathBuf};

use crate::config::ltx_parser::LtxFile;

/// Reads default option values from gamedata/configs/plugins/defaults/
pub struct DefaultsReader {
    ltx: LtxFile,
}

#[allow(dead_code)]
impl DefaultsReader {
    /// Load all defaults by parsing the master includes.ltx file,
    /// which in turn #includes all defaults_*.ltx files via wildcard.
    ///
    /// If `OWL_DEFAULTS_GAMEDATA` is set, defaults are loaded from that
    /// gamedata path instead. This allows dev setups where the dummy game
    /// root has no defaults files but the real mod repo does.
    pub fn load(gamedata_path: &Path) -> Result<Self, String> {
        let effective_gamedata = std::env::var("OWL_DEFAULTS_GAMEDATA")
            .ok()
            .map(PathBuf::from)
            .unwrap_or_else(|| gamedata_path.to_path_buf());

        let includes_path = effective_gamedata
            .join("configs")
            .join("plugins")
            .join("defaults")
            .join("includes.ltx");

        let ltx = LtxFile::parse(&includes_path).map_err(|e| {
            format!(
                "Failed to load defaults from {:?}: {}",
                includes_path, e
            )
        })?;

        Ok(DefaultsReader { ltx })
    }

    /// Get a single default value.
    pub fn get_default(&self, section: &str, key: &str) -> Option<String> {
        self.ltx.get(section, key).map(|s| s.to_string())
    }

    /// Get all defaults for a specific section.
    pub fn get_section_defaults(&self, section: &str) -> Option<HashMap<String, String>> {
        self.ltx.section_entries(section).map(|entries| {
            entries
                .iter()
                .filter_map(|(k, v)| v.as_ref().map(|val| (k.clone(), val.clone())))
                .collect()
        })
    }

    /// Get all defaults organized by section.
    /// Returns HashMap<section_name, HashMap<key, value>>.
    /// Skips entries with empty values.
    pub fn get_all_defaults(&self) -> HashMap<String, HashMap<String, String>> {
        let mut result = HashMap::new();
        for section_name in self.ltx.sections() {
            if let Some(entries) = self.ltx.section_entries(section_name) {
                let section_map: HashMap<String, String> = entries
                    .iter()
                    .filter_map(|(k, v)| v.as_ref().map(|val| (k.clone(), val.clone())))
                    .collect();
                if !section_map.is_empty() {
                    result.insert(section_name.to_string(), section_map);
                }
            }
        }
        result
    }

    /// List all available default sections.
    pub fn sections(&self) -> Vec<String> {
        self.ltx.sections().into_iter().map(|s| s.to_string()).collect()
    }
}
