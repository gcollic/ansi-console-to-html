---
Title: 'ANSI 256 colors table (8-bit)'
Toc:
  Parent: 'ANSI escape sequences'
  Label: '256 colors table'
  Order: 100
---

# ANSI 256 colors table (8-bit)

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit)

## 0-15: named colors

The name of these colors are in the specification, but the actual colors depends on the terminal and user configuration.
0-7 are standard colors, and 8-15 high-intensity versions.

<div>{{include '16-color-table'}}</div>

## 16-231: 216 colors

It's a 6×6×6 color cube, with blue green and red dimensions.
Blue changes at each steps (each column in the example),
green every 6 steps (each 6×6 square in the example),
red every 36 steps (each row  in the example).
The 6 levels in each dimensions are 0, 95, 135, 175, 215 and 255.

<div>{{include '216-color-table'}}</div>

## 232-255: gray

It's a scale of 24 shades of gray, with the level increasing 10 by 10, from 8 to 238.

<div>{{include 'grays-table'}}</div>
