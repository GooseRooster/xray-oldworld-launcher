mod config;
mod game;
mod logging;

use std::collections::HashMap;
use std::sync::RwLock;

use config::launcher_config::LauncherConfig;
use config::user_ltx::UserLtx;
use game::paths::GamePaths;

// -- Application State --

struct AppState {
    paths: RwLock<Option<GamePaths>>,
    launcher_config: RwLock<LauncherConfig>,
}

impl AppState {
    fn new() -> Self {
        AppState {
            paths: RwLock::new(None),
            launcher_config: RwLock::new(LauncherConfig::default()),
        }
    }

    /// Initialize paths and load launcher config.
    fn initialize(&self) -> Result<(), String> {
        // Load launcher config first (it may contain game_root override)
        let launcher_dir = std::env::current_exe()
            .map_err(|e| format!("Failed to get exe path: {}", e))?
            .parent()
            .map(|p| p.to_path_buf())
            .ok_or_else(|| "Failed to get launcher directory".to_string())?;

        // Initialize logging as early as possible
        logging::init(&launcher_dir);
        logging::log("Initializing Old World Launcher...");

        // Dump Proton/Wine/gamescope environment for diagnostics
        logging::log("--- Environment (Proton/Wine/HDR) ---");
        for key in &[
            "PROTON_ENABLE_HDR",
            "PROTON_USE_NTSYNC",
            "DXVK_HDR",
            "ENABLE_GAMESCOPE_WSI",
            "WINEPREFIX",
            "STEAM_COMPAT_DATA_PATH",
            "DISPLAY",
            "WAYLAND_DISPLAY",
            "XDG_CURRENT_DESKTOP",
        ] {
            match std::env::var(key) {
                Ok(val) => logging::log(format!("  {}={}", key, val)),
                Err(_) => logging::log(format!("  {} (not set)", key)),
            }
        }
        logging::log("--- End Environment ---");

        let config = LauncherConfig::load(&launcher_dir);
        logging::log(format!("Launcher config loaded: game_root={:?}", config.game_root));

        // Resolve paths using config's game_root
        let paths = GamePaths::resolve(config.game_root.as_deref())?;

        logging::log("Initialization complete.");

        *self.paths.write().map_err(|e| e.to_string())? = Some(paths);
        *self.launcher_config.write().map_err(|e| e.to_string())? = config;

        Ok(())
    }

    fn get_paths(&self) -> Result<GamePaths, String> {
        self.paths
            .read()
            .map_err(|e| e.to_string())?
            .clone()
            .ok_or_else(|| "Game paths not initialized".to_string())
    }
}

// -- Data Transfer Objects --

#[derive(serde::Serialize, serde::Deserialize)]
#[serde(rename_all = "camelCase")]
struct OptionsState {
    user_ltx: HashMap<String, String>,
}

#[derive(serde::Serialize, serde::Deserialize)]
#[serde(rename_all = "camelCase")]
struct OptionChange {
    path: String,
    value: String,
    #[serde(flatten)]
    storage: OptionStorage,
}

#[derive(serde::Serialize, serde::Deserialize)]
#[serde(rename_all = "camelCase")]
#[serde(tag = "storageType")]
enum OptionStorage {
    #[serde(rename = "userLtx")]
    UserLtx { cmd: String },
}

#[derive(serde::Serialize, serde::Deserialize)]
#[serde(rename_all = "camelCase")]
struct ResetOption {
    path: String,
    default_value: String,
    #[serde(flatten)]
    storage: OptionStorage,
}

// -- Tauri Commands --

#[tauri::command]
fn get_game_paths(state: tauri::State<'_, AppState>) -> Result<GamePaths, String> {
    state.get_paths()
}



#[tauri::command]
fn get_options(state: tauri::State<'_, AppState>) -> Result<OptionsState, String> {
    logging::log("IPC: get_options called");
    let paths = state.get_paths()?;

    let user = UserLtx::load(&paths.appdata);
    let all = user.get_all();
    logging::log(format!("IPC: get_options returning {} commands", all.len()));

    Ok(OptionsState {
        user_ltx: all,
    })
}

#[tauri::command]
fn save_options(
    changes: Vec<OptionChange>,
    state: tauri::State<'_, AppState>,
) -> Result<(), String> {
    let paths = state.get_paths()?;

    let mut user = UserLtx::load(&paths.appdata);
    let mut user_dirty = false;

    for change in changes {
        match change.storage {
            OptionStorage::UserLtx { cmd } => {
                user.set(&cmd, &change.value);
                user_dirty = true;
            }
        }
    }


    if user_dirty {
        user.save(&paths.appdata)?;
    }

    Ok(())
}


#[tauri::command]
fn get_launcher_config(state: tauri::State<'_, AppState>) -> Result<LauncherConfig, String> {
    state
        .launcher_config
        .read()
        .map(|c| c.clone())
        .map_err(|e| e.to_string())
}

#[tauri::command]
fn save_launcher_config(
    config: LauncherConfig,
    state: tauri::State<'_, AppState>,
) -> Result<(), String> {
    let paths = state.get_paths()?;
    config.save(&paths.launcher_dir)?;
    *state
        .launcher_config
        .write()
        .map_err(|e| e.to_string())? = config;
    Ok(())
}

#[tauri::command]
fn launch_game(state: tauri::State<'_, AppState>) -> Result<(), String> {
    let paths = state.get_paths()?;
    let config = state
        .launcher_config
        .read()
        .map_err(|e| e.to_string())?
        .clone();
    game::launcher::launch_game(&paths.game_root, &config)
}

#[tauri::command]
fn clear_shader_cache(state: tauri::State<'_, AppState>) -> Result<u64, String> {
    let paths = state.get_paths()?;
    game::launcher::clear_shader_cache(&paths.appdata)
}

#[tauri::command]
fn reset_user_ltx(state: tauri::State<'_, AppState>) -> Result<(), String> {
    let paths = state.get_paths()?;
    game::launcher::reset_user_ltx(&paths.appdata)
}

#[tauri::command]
fn exit_app(app: tauri::AppHandle) {
    logging::log("Application exit requested.");
    app.exit(0);
}

// -- Tauri Entry Point --

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    let app_state = AppState::new();

    // Attempt initialization but don't fail — paths may not be available yet
    // (user might need to configure game_root through the UI)
    if let Err(e) = app_state.initialize() {
        // Logging may or may not be initialized at this point depending on
        // where the failure occurred. Try to log anyway.
        logging::log(format!("ERROR: Initialization failed: {}", e));
        eprintln!("Old World Launcher: initialization failed: {}", e);
    }

    tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .manage(app_state)
        .invoke_handler(tauri::generate_handler![
            get_game_paths,
            get_options,
            save_options,
            get_launcher_config,
            save_launcher_config,
            launch_game,
            clear_shader_cache,
            reset_user_ltx,
            exit_app,
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
