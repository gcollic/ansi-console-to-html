---
Title: 'ANSI colors'
Toc:
  Parent: 'ANSI escape sequences'
  Label: 'ANSI colors'
  Order: 100
---

# ANSI colors

## Sequences

### 30–37 / 40-47 direct colors

30-37 are for foreground colors, 40-47 are for background colors.
Substract respectively 30 and 40 to get the actual index of the corresponding color in the 256 colors table.

{{include 'ansi_colors-sequence-30-37-40-47'}}

<div class="color-tables">

## 256 colors table (8 bits)

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit)

### 0-15: named colors

The name of these colors are in the specification, but the actual colors depends on the terminal and user configuration.
0-7 are standard colors, and 8-15 high-intensity versions.

<div>{{include '16-color-table'}}</div>

### 16-231: 216 colors

It's a 6×6×6 color cube.

<div>{{include '216-color-table'}}</div>

### 232-255: gray

It's a scale of 24 shades of gray.

<div>{{include 'grays-table'}}</div>

</div>
