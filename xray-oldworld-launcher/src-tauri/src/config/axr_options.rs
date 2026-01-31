use std::collections::HashMap;
use std::path::Path;

use crate::config::ltx_parser::LtxFile;

const OPTIONS_SECTION: &str = "options";

/// Manages reading/writing the axr_options.ltx file.
/// Only modifies the [options] section; preserves all other sections.
pub struct AxrOptions {
    ltx: LtxFile,
}

impl AxrOptions {
    /// Load axr_options.ltx from {game_root}/gamedata/configs/axr_options.ltx.
    /// Returns empty state if file doesn't exist (first launch).
    pub fn load(gamedata_path: &Path) -> Self {
        let path = Self::file_path(gamedata_path);
        let ltx = if path.exists() {
            LtxFile::parse(&path).unwrap_or_else(|_| LtxFile::new())
        } else {
            LtxFile::new()
        };
        AxrOptions { ltx }
    }

    /// Get a value from the [options] section by its hierarchical path key.
    pub fn get(&self, path_key: &str) -> Option<String> {
        self.ltx.get(OPTIONS_SECTION, path_key).map(|s| s.to_string())
    }

    /// Get all values from the [options] section.
    pub fn get_all_options(&self) -> HashMap<String, String> {
        match self.ltx.section_entries(OPTIONS_SECTION) {
            Some(entries) => entries
                .iter()
                .filter_map(|(k, v)| v.as_ref().map(|val| (k.clone(), val.clone())))
                .collect(),
            None => HashMap::new(),
        }
    }

    /// Set a value in the [options] section.
    pub fn set(&mut self, path_key: &str, value: &str) {
        self.ltx.set(OPTIONS_SECTION, path_key, Some(value));
    }

    /// Remove a key from the [options] section.
    pub fn remove(&mut self, path_key: &str) -> bool {
        self.ltx.remove(OPTIONS_SECTION, path_key)
    }

    /// Save the file back to disk, preserving all sections.
    pub fn save(&self, gamedata_path: &Path) -> Result<(), String> {
        let path = Self::file_path(gamedata_path);
        self.ltx.write(&path).map_err(|e| {
            format!("Failed to save axr_options.ltx to {:?}: {}", path, e)
        })
    }

    fn file_path(gamedata_path: &Path) -> std::path::PathBuf {
        gamedata_path.join("configs").join("axr_options.ltx")
    }
}
