---
Title: 'ANSI text decorations sequences'
Toc:
  Parent: 'ANSI escape sequences'
  Label: 'Decorations sequences'
  Order: 4
---

# ANSI text decorations sequences

## Underline

Style and color options have been added to the classic underline code sequences.
It was originally added by Kitty, now also adopted by other terminals, and supported by applications such as vim.

See [https://sw.kovidgoyal.net/kitty/underlines/](https://sw.kovidgoyal.net/kitty/underlines/)

`4` (underline) and `24` (no underline) are classic codes for underlines.

`21` is double-underline per ECMA-48, but instead disables bold intensity on some terminals, including in the Linux kernel's console <em>before version 4.17</em>.

`4:x` are the new extensions.

{{include 'ansi_text_decorations_underline'}}

For underline color, see the {{link_to 'ansi_colors_sequences' 'colors sequences'}}.
