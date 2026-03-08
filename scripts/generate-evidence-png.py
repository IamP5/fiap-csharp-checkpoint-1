#!/usr/bin/env python3
import argparse
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont

ROOT = Path(__file__).resolve().parents[1]

parser = argparse.ArgumentParser()
parser.add_argument("--input", default=str(ROOT / "docs" / "evidence"))
parser.add_argument("--output", default=str(ROOT / "docs" / "prints"))
args = parser.parse_args()

EVIDENCE_DIR = Path(args.input)
PRINTS_DIR = Path(args.output)
PRINTS_DIR.mkdir(parents=True, exist_ok=True)

MAP = {
    "menu.txt": "menu.png",
    "adicao.txt": "adicao.png",
    "subtracao.txt": "subtracao.png",
    "multiplicacao.txt": "multiplicacao.png",
    "divisao.txt": "divisao.png",
    "divisao_por_zero.txt": "divisao_por_zero.png",
}

font = None
for candidate in [
    "/usr/share/fonts/truetype/dejavu/DejaVuSansMono.ttf",
    "/usr/share/fonts/dejavu/DejaVuSansMono.ttf",
]:
    p = Path(candidate)
    if p.exists():
        font = ImageFont.truetype(str(p), 22)
        break
if font is None:
    font = ImageFont.load_default()

padding = 32
line_spacing = 12
bg = (12, 12, 16)
fg = (226, 232, 240)

for src_name, out_name in MAP.items():
    src = EVIDENCE_DIR / src_name
    if not src.exists():
        continue

    text = src.read_text(encoding="utf-8", errors="replace").replace("\t", "    ")
    lines = text.splitlines() or [""]

    dummy = Image.new("RGB", (10, 10))
    draw = ImageDraw.Draw(dummy)

    widths = []
    heights = []
    for line in lines:
        left, top, right, bottom = draw.textbbox((0, 0), line if line else " ", font=font)
        widths.append(right - left)
        heights.append(bottom - top)

    line_height = max(heights) if heights else 20
    img_w = max(widths) + padding * 2
    img_h = len(lines) * (line_height + line_spacing) - line_spacing + padding * 2

    img = Image.new("RGB", (max(1000, img_w), max(500, img_h)), bg)
    draw = ImageDraw.Draw(img)

    y = padding
    for line in lines:
        draw.text((padding, y), line, font=font, fill=fg)
        y += line_height + line_spacing

    img.save(PRINTS_DIR / out_name)
    try:
        rel = (PRINTS_DIR / out_name).relative_to(ROOT)
    except ValueError:
        rel = (PRINTS_DIR / out_name)
    print(f"Generated {rel}")
