#!/usr/bin/env bash
# Dev launcher with game path overrides.
# Sets env vars so the Tauri binary can find game files during development.
#
# Usage: ./dev.sh   or   npm run dev:env

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Dummy game root within the project (writable — axr_options.ltx, user.ltx)
export OWL_GAME_ROOT="$SCRIPT_DIR/dev-gameroot"

# Keep dev-gameroot's user_default.ltx in sync with the repo copy
mkdir -p "$OWL_GAME_ROOT"
cp "$SCRIPT_DIR/user_default.ltx" "$OWL_GAME_ROOT/user_default.ltx"

echo "OWL_GAME_ROOT       = $OWL_GAME_ROOT"

npx tauri dev
