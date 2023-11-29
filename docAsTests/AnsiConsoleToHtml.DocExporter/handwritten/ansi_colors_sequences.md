---
Title: 'ANSI colors sequences'
Toc:
  Parent: 'ANSI escape sequences'
  Label: 'Colors sequences'
  Order: 3
---

# ANSI colors sequences

## 4-bit colors: 30-37 / 40-47 / 90-97 / 100-107

Substract 'x' to get the actual index in the {{link_to 'ansi_colors_table' '256 colors table'}}.

* Standard colors
    * foreground: 30-37 (substract 30 for color index)
    * background: 40-47 (substract 40 for color index)
* Bright colors
    * foreground: 90-97 (substract 82 for color index)
    * background: 100-107 (substract 92 for color index)

{{include 'ansi_colors_sequences-4bit-colors'}}

## 8-bit colors: 38;5;n / 48;5;n

`n` is the index in the {{link_to 'ansi_colors_table' '256 colors table'}}.

{{include 'ansi_colors_sequences-8bit-colors'}}

## 24-bit colors: 38;2;r;g;b / 48;2;r;g;b

`r` `g` and `b` are the red, green, and blue components of the RGB color space (each between 0 and 255).

{{include 'ansi_colors_sequences-24bit-colors'}}

## Underline color

By default, the underline color is the same as the foreground color.
To set a different color, the code `58` is used, and works exactly like the codes `38` and `48`.

See {{link_to 'ansi_text_decorations' 'text decorations'}} for more underline details.

{{include 'ansi_colors_sequences_underline'}}