#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
OUT_DIR="$ROOT_DIR/docs/evidence"
APP_DLL="$ROOT_DIR/bin/Release/net8.0/fiap-checkpoint1-calculadora.dll"
DOTNET_BIN="${DOTNET_BIN:-$HOME/.dotnet/dotnet}"

mkdir -p "$OUT_DIR"

if [[ ! -f "$APP_DLL" ]]; then
  echo "Build not found, running release build..."
  "$DOTNET_BIN" build "$ROOT_DIR" -c Release >/dev/null
fi

run_case() {
  local name="$1"
  local input="$2"

  printf "%b" "$input" | "$DOTNET_BIN" "$APP_DLL" >"$OUT_DIR/${name}.txt" || true
  echo "Generated: $OUT_DIR/${name}.txt"
}

# Note: each run exits through option 5
run_case "menu" "5\n"
run_case "adicao" "1\n10\n5\n\n5\n"
run_case "subtracao" "2\n10\n5\n\n5\n"
run_case "multiplicacao" "3\n10\n5\n\n5\n"
run_case "divisao" "4\n10\n5\n\n5\n"
run_case "divisao_por_zero" "4\n10\n0\n\n5\n"

echo "Done. Use these logs to quickly capture terminal screenshots if needed."
