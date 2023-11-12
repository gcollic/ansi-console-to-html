﻿---
Title: 'ANSI 256 colors table'
Navbar:
  Label: 'Docs'
  Order: 1
Toc:
  Parent: 'ANSI commands'
  Label: 'ANSI 256 colors table'
  Order: 1
---

# 256 colors table (8 bits)

Cf. [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#8-bit)

## 0-15: named colors

The name of these colors are in the specification, but the actual colors depends on the terminal and user configuration.
0-7 are standard colors, and 8-15 high-intensity versions.

<div>{{include '16-color-table'}}</div>

## 16-231: 216 colors

It's a 6×6×6 color cube.

<div>{{include '216-color-table'}}</div>

## 232-255: gray

It's a scale of 24 shades of gray.

<div>{{include 'grays-table'}}</div>
