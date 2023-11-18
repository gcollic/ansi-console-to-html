---
Title: 'Graphic sequence overview'
Toc:
  Parent: 'ANSI escape sequences'
  Label: 'Graphic sequence overview'
  Order: 2
---

# Graphic sequence overview

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#SGR_(Select_Graphic_Rendition)_parameters)

The control sequence <code style='color:#A31515;'>"\x1B[ n m"</code>, named Select Graphic Rendition (SGR), sets display attributes.
Several attributes can be set in the same sequence, separated by semicolons.
Each display attribute remains in effect until a following occurrence of SGR resets it.

## Overview

{{include 'ansi-graphic-sequences_overview'}}

