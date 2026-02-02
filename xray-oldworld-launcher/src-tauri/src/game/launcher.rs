use std::fs;
use std::path::Path;
use std::process::Command;

use crate::config::launcher_config::LauncherConfig;
use crate::logging;

/// Launch the game executable with configured arguments.
pub fn launch_game(game_root: &Path, config: &LauncherConfig) -> Result<(), String> {
    let exe_name = if config.use_avx {
        "OldWorldDX11AVX.exe"
    } else {
        "OldWorldDX11.exe"
    };

    let exe_path = game_root.join("bin").join(exe_name);
    if !exe_path.exists() {
        logging::log(format!("ERROR: Game executable not found: {}", exe_path.display()));
        return Err(format!("Game executable not found: {:?}", exe_path));
    }

    let mut args: Vec<String> = Vec::new();

    // Shadow map size
    if config.shadow_map_size != 2048 {
        args.push(format!("-smap{}", config.shadow_map_size));
    }

    // Debug mode
    if config.debug_mode {
        args.push("-dbg".to_string());
    }

    // Custom arguments (split on whitespace)
    let custom = config.custom_args.trim();
    if !custom.is_empty() {
        for arg in custom.split_whitespace() {
            args.push(arg.to_string());
        }
    }

    logging::log("--- Game Launch ---");
    logging::log(format!("Exe:  {}", exe_path.display()));
    logging::log(format!("Args: {:?}", args));
    logging::log(format!("CWD:  {}", game_root.display()));

    

    let mut cmd = Command::new(&exe_path);
    cmd.args(&args).current_dir(game_root);

    

    cmd.spawn().map_err(|e| {
        logging::log(format!("ERROR: Failed to launch: {}", e));
        format!("Failed to launch {:?}: {}", exe_path, e)
    })?;

    logging::log("Game process spawned successfully.");
    Ok(())
}

/// Delete the shader cache directory and return the number of bytes freed.
pub fn clear_shader_cache(appdata_path: &Path) -> Result<u64, String> {
    let cache_dir = appdata_path.join("shaders_cache");
    if !cache_dir.exists() {
        return Ok(0);
    }

    let bytes = dir_size(&cache_dir);
    fs::remove_dir_all(&cache_dir)
        .map_err(|e| format!("Failed to delete shader cache at {:?}: {}", cache_dir, e))?;

    Ok(bytes)
}

/// Delete user.ltx so the game regenerates it with defaults on next launch.
pub fn reset_user_ltx(appdata_path: &Path) -> Result<(), String> {
    let user_ltx = appdata_path.join("user.ltx");
    if user_ltx.exists() {
        fs::remove_file(&user_ltx)
            .map_err(|e| format!("Failed to delete user.ltx at {:?}: {}", user_ltx, e))?;
    }
    Ok(())
}

/// Calculate total size of a directory recursively.
fn dir_size(path: &Path) -> u64 {
    let mut total = 0u64;
    if let Ok(entries) = fs::read_dir(path) {
        for entry in entries.flatten() {
            let entry_path = entry.path();
            if entry_path.is_dir() {
                total += dir_size(&entry_path);
            } else if let Ok(metadata) = entry.metadata() {
                total += metadata.len();
            }
        }
    }
    total
}
