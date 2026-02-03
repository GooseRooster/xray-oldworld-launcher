use serde::{Deserialize, Serialize};
use std::fs;
use std::path::Path;

/// Launcher-specific configuration stored as JSON next to the launcher executable.
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct LauncherConfig {
    #[serde(default)]
    pub use_avx: bool,
    #[serde(default)]
    pub debug_mode: bool,
    #[serde(default = "default_shadow_map_size")]
    pub shadow_map_size: u32,
    #[serde(default)]
    pub custom_args: String,
    #[serde(default)]
    pub game_root: Option<String>,
    #[serde(default = "default_language")]
    pub language: String,
    #[serde(default)]
    pub linux_custom_command: Option<String>,
}

fn default_shadow_map_size() -> u32 {
    2048
}

fn default_language() -> String {
    "en".to_string()
}

impl Default for LauncherConfig {
    fn default() -> Self {
        LauncherConfig {
            use_avx: false,
            debug_mode: false,
            shadow_map_size: 2048,
            custom_args: String::new(),
            game_root: None,
            language: default_language(),
            linux_custom_command: None,
        }
    }
}

impl LauncherConfig {
    const FILENAME: &'static str = "launcher_config.json";

    /// Load config from the launcher directory. Returns default if file doesn't exist.
    pub fn load(launcher_dir: &Path) -> Self {
        let path = launcher_dir.join(Self::FILENAME);
        if !path.exists() {
            return Self::default();
        }
        match fs::read_to_string(&path) {
            Ok(content) => serde_json::from_str(&content).unwrap_or_default(),
            Err(_) => Self::default(),
        }
    }

    /// Save config to the launcher directory.
    pub fn save(&self, launcher_dir: &Path) -> Result<(), String> {
        let path = launcher_dir.join(Self::FILENAME);
        let content = serde_json::to_string_pretty(self)
            .map_err(|e| format!("Failed to serialize launcher config: {}", e))?;
        fs::write(&path, content)
            .map_err(|e| format!("Failed to write launcher config to {:?}: {}", path, e))
    }
}
