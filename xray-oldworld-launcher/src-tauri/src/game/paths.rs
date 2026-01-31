use std::env;
use std::path::{Path, PathBuf};

#[derive(Debug, Clone, serde::Serialize)]
#[serde(rename_all = "camelCase")]
pub struct GamePaths {
    pub game_root: PathBuf,
    pub appdata: PathBuf,
    pub gamedata: PathBuf,
    pub bin: PathBuf,
    pub launcher_dir: PathBuf,
}

impl GamePaths {
    /// Resolve all game paths.
    ///
    /// Priority for game_root:
    /// 1. `OWL_GAME_ROOT` environment variable (for dev/testing)
    /// 2. `config_game_root` parameter (from launcher_config.json)
    /// 3. Relative to launcher executable (parent directory)
    pub fn resolve(config_game_root: Option<&str>) -> Result<Self, String> {
        let launcher_dir = Self::get_launcher_dir()?;
        let game_root = Self::resolve_game_root(config_game_root, &launcher_dir)?;
        let appdata = Self::resolve_appdata(&game_root);
        let gamedata = game_root.join("gamedata");
        let bin = game_root.join("bin");

        Ok(GamePaths {
            game_root,
            appdata,
            gamedata,
            bin,
            launcher_dir,
        })
    }

    fn get_launcher_dir() -> Result<PathBuf, String> {
        env::current_exe()
            .map_err(|e| format!("Failed to get launcher executable path: {}", e))?
            .parent()
            .map(|p| p.to_path_buf())
            .ok_or_else(|| "Failed to get launcher directory".to_string())
    }

    fn resolve_game_root(
        config_game_root: Option<&str>,
        launcher_dir: &Path,
    ) -> Result<PathBuf, String> {
        // 1. Environment variable override (highest priority, for dev/testing)
        if let Ok(env_root) = env::var("OWL_GAME_ROOT") {
            let path = PathBuf::from(&env_root);
            if path.is_dir() {
                return Ok(path);
            }
            return Err(format!(
                "OWL_GAME_ROOT is set to '{}' but directory does not exist",
                env_root
            ));
        }

        // 2. Config file override
        if let Some(root) = config_game_root {
            if !root.is_empty() {
                let path = PathBuf::from(root);
                if path.is_dir() {
                    return Ok(path);
                }
                return Err(format!(
                    "launcher_config.json game_root is set to '{}' but directory does not exist",
                    root
                ));
            }
        }

        // 3. Relative to launcher exe (launcher is expected inside game directory)
        // The launcher binary lives in the game root or a subdirectory of it.
        // Try launcher_dir itself first, then parent.
        if launcher_dir.join("gamedata").is_dir() {
            return Ok(launcher_dir.to_path_buf());
        }
        if let Some(parent) = launcher_dir.parent() {
            if parent.join("gamedata").is_dir() {
                return Ok(parent.to_path_buf());
            }
        }

        Err(
            "Could not resolve game root. Set OWL_GAME_ROOT environment variable or configure game_root in launcher settings.".to_string()
        )
    }

    /// Resolve appdata directory.
    /// Checks {game_root}/appdata/ first (portable installs), then falls back.
    fn resolve_appdata(game_root: &Path) -> PathBuf {
        let local_appdata = game_root.join("appdata");
        if local_appdata.is_dir() {
            return local_appdata;
        }
        // Fallback: try platform-standard location
        // On Windows, Anomaly typically uses %APPDATA%/Stalker-COP or similar,
        // but the portable appdata/ inside game root is most common for modded installs.
        // Default to the local path even if it doesn't exist yet (it will be created).
        local_appdata
    }
}
