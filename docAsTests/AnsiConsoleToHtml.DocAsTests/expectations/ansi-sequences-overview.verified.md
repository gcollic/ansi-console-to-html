---
Title: 'ANSI sequences overview'
Toc:
  Parent: 'ANSI escape sequences'
  Label: 'Overview'
  Order: 1
---

# ANSI sequences overview

Only the <abbr title="Select Graphic Rendition">SGR</abbr> sequence is supported. The other ANSI escape sequences are ignored.

## Graphic sequence

References: [Wikipedia](https://en.wikipedia.org/wiki/ANSI_escape_code#SGR_(Select_Graphic_Rendition)_parameters)

The control sequence <code style='color:#A31515;'>"\x1B[ n m"</code>, named Select Graphic Rendition (SGR), sets display attributes.
Several attributes can be set in the same sequence, separated by semicolons.
Each display attribute remains in effect until a following occurrence of SGR resets it.

{{include 'ansi-sequences-overview_graphic'}}

## Non-graphic sequences

{{include 'ansi-sequences-overview_non_graphic'}}

