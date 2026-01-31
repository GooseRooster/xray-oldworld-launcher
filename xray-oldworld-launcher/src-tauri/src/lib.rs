mod config;
mod game;

use std::collections::HashMap;
use std::sync::RwLock;

use config::axr_options::AxrOptions;
use config::defaults_reader::DefaultsReader;
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

        let config = LauncherConfig::load(&launcher_dir);

        // Resolve paths using config's game_root
        let paths = GamePaths::resolve(config.game_root.as_deref())?;

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
    axr_options: HashMap<String, String>,
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
    #[serde(rename = "axrOptions")]
    AxrOptions,
    #[serde(rename = "userLtx")]
    UserLtx { cmd: String },
}

#[derive(serde::Serialize, serde::Deserialize)]
#[serde(rename_all = "camelCase")]
struct ResetRequest {
    options: Vec<ResetOption>,
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
fn get_all_defaults(state: tauri::State<'_, AppState>) -> Result<HashMap<String, HashMap<String, String>>, String> {
    let paths = state.get_paths()?;
    let reader = DefaultsReader::load(&paths.gamedata)?;
    Ok(reader.get_all_defaults())
}

#[tauri::command]
fn get_options(state: tauri::State<'_, AppState>) -> Result<OptionsState, String> {
    let paths = state.get_paths()?;

    let axr = AxrOptions::load(&paths.gamedata);
    let user = UserLtx::load(&paths.appdata);

    Ok(OptionsState {
        axr_options: axr.get_all_options(),
        user_ltx: user.get_all(),
    })
}

#[tauri::command]
fn save_options(
    changes: Vec<OptionChange>,
    state: tauri::State<'_, AppState>,
) -> Result<(), String> {
    let paths = state.get_paths()?;

    let mut axr = AxrOptions::load(&paths.gamedata);
    let mut user = UserLtx::load(&paths.appdata);
    let mut axr_dirty = false;
    let mut user_dirty = false;

    for change in changes {
        match change.storage {
            OptionStorage::AxrOptions => {
                axr.set(&change.path, &change.value);
                axr_dirty = true;
            }
            OptionStorage::UserLtx { cmd } => {
                user.set(&cmd, &change.value);
                user_dirty = true;
            }
        }
    }

    if axr_dirty {
        axr.save(&paths.gamedata)?;
    }
    if user_dirty {
        user.save(&paths.appdata)?;
    }

    Ok(())
}

#[tauri::command]
fn reset_options_to_defaults(
    options: Vec<ResetOption>,
    state: tauri::State<'_, AppState>,
) -> Result<(), String> {
    let paths = state.get_paths()?;

    let mut axr = AxrOptions::load(&paths.gamedata);
    let mut user = UserLtx::load(&paths.appdata);
    let mut axr_dirty = false;
    let mut user_dirty = false;

    for opt in options {
        match opt.storage {
            OptionStorage::AxrOptions => {
                axr.set(&opt.path, &opt.default_value);
                axr_dirty = true;
            }
            OptionStorage::UserLtx { cmd } => {
                user.set(&cmd, &opt.default_value);
                user_dirty = true;
            }
        }
    }

    if axr_dirty {
        axr.save(&paths.gamedata)?;
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

// -- Tauri Entry Point --

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    let app_state = AppState::new();

    // Attempt initialization but don't fail — paths may not be available yet
    // (user might need to configure game_root through the UI)
    let _ = app_state.initialize();

    tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .manage(app_state)
        .invoke_handler(tauri::generate_handler![
            get_game_paths,
            get_all_defaults,
            get_options,
            save_options,
            reset_options_to_defaults,
            get_launcher_config,
            save_launcher_config,
            launch_game,
            clear_shader_cache,
            reset_user_ltx,
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
