use std::fs::{File, OpenOptions};
use std::io::Write;
use std::path::{Path, PathBuf};
use std::sync::Mutex;

static LOG_PATH: Mutex<Option<PathBuf>> = Mutex::new(None);

/// Initialize the log file, truncating any previous contents.
/// Call this as early as possible in the application lifecycle.
pub fn init(launcher_dir: &Path) {
    let log_path = launcher_dir.join("owl_launcher.log");

    if let Ok(mut f) = File::create(&log_path) {
        let _ = writeln!(f, "=== Old World Launcher ===");
        let _ = writeln!(f, "Launcher dir: {}", launcher_dir.display());
        let _ = writeln!(f);
    }

    if let Ok(mut guard) = LOG_PATH.lock() {
        *guard = Some(log_path);
    }
}

/// Append a line to the log file.
pub fn log(message: impl std::fmt::Display) {
    if let Ok(guard) = LOG_PATH.lock() {
        if let Some(path) = guard.as_ref() {
            if let Ok(mut f) = OpenOptions::new().append(true).open(path) {
                let _ = writeln!(f, "{}", message);
            }
        }
    }
}
