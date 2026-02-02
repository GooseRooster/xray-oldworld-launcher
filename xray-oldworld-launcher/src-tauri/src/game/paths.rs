use std::env;
use std::path::{Path, PathBuf};

use crate::logging;

#[derive(Debug, Clone, serde::Serialize)]
#[serde(rename_all = "camelCase")]
pub struct GamePaths {
    pub game_root: PathBuf,
    pub appdata: PathBuf,
    pub bin: PathBuf,
    pub launcher_dir: PathBuf,
}

impl GamePaths {
    /// Resolve all game paths.
    ///
    /// Priority for game_root:
    /// 1. `OWL_GAME_ROOT` environment variable (for dev/testing)
    /// 2. `config_game_root` from launcher_config.json (user override)
    /// 3. Relative to launcher executable (parent directory)
    pub fn resolve(config_game_root: Option<&str>) -> Result<Self, String> {
        logging::log("--- Path Resolution ---");

        let launcher_dir = Self::get_launcher_dir()?;
        logging::log(format!("Launcher dir: {}", launcher_dir.display()));

        // Log canonicalized path for Proton/symlink debugging
        if let Ok(canonical) = launcher_dir.canonicalize() {
            if canonical != launcher_dir {
                logging::log(format!("Launcher dir (canonical): {}", canonical.display()));
            }
        }

        let game_root = Self::resolve_game_root(&launcher_dir, config_game_root)?;
        let appdata = Self::resolve_appdata(&game_root);
        let bin = game_root.join("bin");

        logging::log(format!("Game root: {}", game_root.display()));
        logging::log(format!("Appdata:   {}", appdata.display()));
        logging::log(format!("Bin:       {}", bin.display()));
        logging::log(format!(
            "Appdata exists: {}, Bin exists: {}",
            appdata.is_dir(),
            bin.is_dir()
        ));

        let user_ltx_path = appdata.join("user.ltx");
        logging::log(format!(
            "user.ltx path: {} (exists: {})",
            user_ltx_path.display(),
            user_ltx_path.exists()
        ));
        logging::log("--- End Path Resolution ---");

        Ok(GamePaths {
            game_root,
            appdata,
            bin,
            launcher_dir,
        })
    }

    fn get_launcher_dir() -> Result<PathBuf, String> {
        let exe_path = env::current_exe()
            .map_err(|e| format!("Failed to get launcher executable path: {}", e))?;
        logging::log(format!("current_exe(): {}", exe_path.display()));

        exe_path
            .parent()
            .map(|p| p.to_path_buf())
            .ok_or_else(|| "Failed to get launcher directory".to_string())
    }

    fn resolve_game_root(
        launcher_dir: &Path,
        config_game_root: Option<&str>,
    ) -> Result<PathBuf, String> {
        // 1. Environment variable override (highest priority, for dev/testing)
        if let Ok(env_root) = env::var("OWL_GAME_ROOT") {
            let path = PathBuf::from(&env_root);
            logging::log(format!("OWL_GAME_ROOT set to: {}", env_root));
            if path.is_dir() {
                logging::log("Using OWL_GAME_ROOT (valid directory)");
                return Ok(path);
            }
            return Err(format!(
                "OWL_GAME_ROOT is set to '{}' but directory does not exist",
                env_root
            ));
        }

        // 2. Config file override (from launcher_config.json)
        if let Some(root) = config_game_root {
            let path = PathBuf::from(root);
            logging::log(format!("Config game_root: {}", root));
            if path.is_dir() && path.join("gamedata").is_dir() {
                logging::log("Using config game_root (valid directory with gamedata)");
                return Ok(path);
            }
            logging::log(format!(
                "WARNING: config game_root '{}' is not valid (exists: {}, has gamedata: {})",
                root,
                path.is_dir(),
                path.join("gamedata").is_dir()
            ));
        }

        // 3. Relative to launcher exe (launcher is expected inside game directory)
        // Try launcher_dir itself first, then parent.
        logging::log(format!(
            "Checking launcher_dir for gamedata: {}",
            launcher_dir.join("gamedata").display()
        ));
        if launcher_dir.join("gamedata").is_dir() {
            logging::log("Game root = launcher_dir (gamedata found)");
            return Ok(launcher_dir.to_path_buf());
        }

        if let Some(parent) = launcher_dir.parent() {
            logging::log(format!(
                "Checking parent for gamedata: {}",
                parent.join("gamedata").display()
            ));
            if parent.join("gamedata").is_dir() {
                logging::log("Game root = launcher_dir parent (gamedata found)");
                return Ok(parent.to_path_buf());
            }
        }

        let err = "Could not resolve game root. Set OWL_GAME_ROOT environment variable or configure game_root in launcher settings.";
        logging::log(format!("ERROR: {}", err));
        Err(err.to_string())
    }

    /// Resolve appdata directory.
    fn resolve_appdata(game_root: &Path) -> PathBuf {
        let local_appdata = game_root.join("appdata");
        if local_appdata.is_dir() {
            return local_appdata;
        }

        logging::log(format!(
            "WARNING: appdata directory does not exist at {}",
            local_appdata.display()
        ));
        local_appdata
    }
}
