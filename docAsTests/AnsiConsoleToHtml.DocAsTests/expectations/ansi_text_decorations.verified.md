---
Title: 'ANSI text decorations sequences'
Toc:
  Parent: 'ANSI escape sequences'
  Label: 'Decorations sequences'
  Order: 4
---

# ANSI text decorations sequences

## Underline

4 (underline) and 24 (no underline) are classic codes for underlines.

21 is double-underline per ECMA-48, but instead disables bold intensity on some terminals, including in the Linux kernel'sconsole <em>before version 4.17</em>.

More recently, options have been added to the code sequence 4.
It was originally added by Kitty, now also adopted by other terminals, and supported by applications such as vim.

See [https://sw.kovidgoyal.net/kitty/underlines/](https://sw.kovidgoyal.net/kitty/underlines/)

{{include 'ansi_text_decorations_underline'}}

