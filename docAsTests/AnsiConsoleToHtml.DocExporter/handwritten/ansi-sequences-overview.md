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

The control sequence `ESC [ n m` (where `n` is one of the codes below) is named Select Graphic Rendition (SGR).
It sets display attributes. Several attributes can be set in the same sequence, separated by semicolons,
and (rarely) with options separated by colons.
Each display attribute remains in effect until a following occurrence of SGR explicitely resets it.

{{include 'ansi-sequences-overview_graphic'}}

## Non-graphic sequences

{{include 'ansi-sequences-overview_non_graphic'}}

