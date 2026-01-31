#!/usr/bin/env bash
# Dev launcher with game path overrides.
# Sets env vars so the Tauri binary can find game files during development.
#
# Usage: ./dev.sh   or   npm run dev:env

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Dummy game root within the project (writable — axr_options.ltx, user.ltx)
export OWL_GAME_ROOT="$SCRIPT_DIR/dev-gameroot"

# Real defaults from the OWA mod repo (read-only — defaults_*.ltx)
export OWL_DEFAULTS_GAMEDATA="/home/gooze/Modding/S.T.A.L.K.E.R. Mods/OWA - DEV/oldworld/OWA Build 153/OWA 00. Base files/gamedata"

echo "OWL_GAME_ROOT       = $OWL_GAME_ROOT"
echo "OWL_DEFAULTS_GAMEDATA = $OWL_DEFAULTS_GAMEDATA"

npx tauri dev
