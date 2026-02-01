# Old World Launcher

Desktop launcher for Old World Addon, a S.T.A.L.K.E.R. Anomaly gameplay and atmosphere overhaul. Built with **Tauri 2** (Rust backend) and **Blazor WebAssembly** (.NET 10 frontend).

The launcher provides a native UI for configuring game settings before first launch — particularly video, sound, and control options that map to `user.ltx` console commands. After the game has been launched once, the in-game options menu takes over for fine-tuning.

## What it covers

| Page | Scope |
|------|-------|
| **Main** | Launch configuration (AVX toggle, debug mode), game launch, shader cache clearing, user.ltx reset |
| **Video** | Basic settings (FOV, screen mode, HDR, lighting style) + advanced rendering (SSAO, grass, shadows — settings that trigger shader recompilation) |
| **Sound** | Volumes, EAX, dynamic music, captions |
| **Controls** | Mouse sensitivity, toggle options, pickup/PDA modes |

Only options backed by a `cmd=` console command are included. HUD tuning, SSFX detail, night settings, and other in-game-only options are intentionally excluded.

## Tech stack

- **Backend**: Rust via Tauri 2 (`xray-oldworld-launcher/src-tauri/`)
- **Frontend**: Blazor WebAssembly on .NET 10.0 (`xray-oldworld-launcher/src/`)
- **UI library**: MudBlazor
- **IPC bridge**: `SyminStudio.TauriApi` + `__TAURI__.core.invoke`

## Prerequisites

- **Node.js** (LTS) with npm
- **Rust** stable toolchain
- **.NET 10.0 SDK**
- **Linux only**: system libraries for Tauri's WebKit2GTK backend:
  ```
  sudo apt install libwebkit2gtk-4.1-dev libayatana-appindicator3-dev librsvg2-dev
  ```

## Development

All commands run from `xray-oldworld-launcher/`:

```bash
# Install Tauri CLI
npm install

# Start dev mode (Blazor hot-reload + Tauri window)
npm run dev

# Or with game path overrides for local testing:
npm run dev:env
```

`dev:env` runs `dev.sh`, which sets `OWL_GAME_ROOT` environment variable so the Rust backend can locate game files without a full game installation. Edit `dev.sh` to point at your local paths.

## Production build

```bash
npm run build
```

This runs `dotnet publish` (Blazor WASM assets) then `tauri build` (Rust compilation + bundling). The output binary embeds all frontend assets — no external files needed.

## CI

GitHub Actions builds on every push that touches `xray-oldworld-launcher/`:

| Runner | Output |
|--------|--------|
| `windows-latest` | Standalone `.exe` |
| `ubuntu-22.04` | `.AppImage` (self-contained, no system deps required) |

Artifacts are downloadable from the Actions tab. Output filenames are configured at the top of `.github/workflows/build.yml`.

## Project structure

```
Old World Launcher/
├── .github/workflows/
│   ├── build.yml              # CI build (Windows + Linux)
│   └── discord-notify.yml     # Commit notifications
├── xray-oldworld-launcher/
│   ├── src/                   # Blazor WebAssembly frontend
│   │   ├── Components/Options/   # Reusable option controls
│   │   ├── Models/                # Data models
│   │   ├── OptionDefinitions/     # Video/Sound/Control page definitions
│   │   ├── Pages/                 # Routable pages (Main, Video, Sound, Control)
│   │   └── Services/              # State management, localization
│   ├── src-tauri/             # Rust backend
│   │   └── src/
│   │       ├── config/            # LTX parser, launcher config, user.ltx I/O
│   │       └── game/              # Game paths, process launcher
│   ├── package.json           # npm scripts (dev, build)
│   └── dev.sh                 # Dev launcher with env overrides
└── README.md
```
