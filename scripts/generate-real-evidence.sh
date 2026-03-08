#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
EVID_DIR="$ROOT_DIR/docs/evidence-real"
PRINTS_DIR="$ROOT_DIR/docs/prints"
DOTNET_BIN="${DOTNET_BIN:-$HOME/.dotnet/dotnet}"

mkdir -p "$EVID_DIR" "$PRINTS_DIR"

run_case() {
  local name="$1"
  local input="$2"

  # Use `script` to run inside a pseudo terminal so typed input is echoed naturally.
  printf "%b" "$input" | script -q -c "$DOTNET_BIN run --project '$ROOT_DIR/fiap-checkpoint1-calculadora.csproj' -c Release" "$EVID_DIR/${name}.typescript" >/dev/null

  # Normalize terminal control sequences while preserving what appeared on screen.
  col -b < "$EVID_DIR/${name}.typescript" > "$EVID_DIR/${name}.txt"
  echo "Generated: $EVID_DIR/${name}.txt"
}

run_case "menu" "5\n"
run_case "adicao" "1\n10\n5\n\n5\n"
run_case "subtracao" "2\n10\n5\n\n5\n"
run_case "multiplicacao" "3\n10\n5\n\n5\n"
run_case "divisao" "4\n10\n5\n\n5\n"
run_case "divisao_por_zero" "4\n10\n0\n\n5\n"

echo "Rendering PNGs..."
"$ROOT_DIR/.venv/bin/python" "$ROOT_DIR/scripts/generate-evidence-png.py" --input "$EVID_DIR" --output "$PRINTS_DIR"

echo "Done. Real execution evidences are in docs/evidence-real and docs/prints."
