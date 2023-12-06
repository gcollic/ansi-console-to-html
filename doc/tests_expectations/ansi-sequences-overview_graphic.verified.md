﻿
| n | Description | Example | Rendered |
|---|-------------|---------|----------|
| 0 | Reset | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[0mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span>World</pre> |
| 1 | Bold or intense | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[1mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='font-weight:900'>World</span></pre> |
| 2 | Faint/Dim | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[2mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:#7F7F7F'>World</span></pre> |
| 3 | Italic | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[3mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='font-style:italic'>World</span></pre> |
| 4 | Underline (with optional style)<br/>{{link_to 'ansi_text_decorations' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[4mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='text-decoration:underline 1px solid'>World</span></pre> |
| 7 | Inverse colors<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[7mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:#000000;background:#FFFFFF'>World</span></pre> |
| 8 | Hidden (but selectable) | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[8mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:transparent'>World</span></pre> |
| 9 | Crossed-out | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[9mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='text-decoration:line-through'>World</span></pre> |
| 21 | Doubly underlined<br/>{{link_to 'ansi_text_decorations' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[21mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='text-decoration:underline 1px double'>World</span></pre> |
| 22 | Neither bold nor faint | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[22mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#BBBB00;background:#0000BB;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>World</span></span></pre> |
| 23 | Not italic | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[23mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#5D5D00;background:#0000BB;font-weight:900;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>World</span></span></pre> |
| 24 | Not underlined<br/>{{link_to 'ansi_text_decorations' 'more details'}} | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[24mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'>World</span></pre> |
| 27 | Not inversed<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[7mHi <span style="color:#EE0000">\x1B</span>[27mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#000000;background:#FFFFFF'>Hi </span>World</pre> |
| 28 | Not hidden | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[8mHi <span style="color:#EE0000">\x1B</span>[28mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:transparent'>Hi </span>World</pre> |
| 29 | Not crossed out | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[29mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:underline 1px solid #BB0000'>World</span></pre> |
| 30–37 | Set foreground color (standard)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[32mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:#00BB00'>World</span></pre> |
| 38 | Set foreground color (38;5;n or 38;2;r;g;b)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[38;2;110;120;170mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:#6E78AA'>World</span></pre> |
| 39 | Default foreground color | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[39mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#7F7F7F;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>World</span></span></pre> |
| 40–47 | Set background color (standard)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[42mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='background:#00BB00'>World</span></pre> |
| 48 | Set background color (48;5;n or 48;2;r;g;b)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[48;2;110;120;170mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='background:#6E78AA'>World</span></pre> |
| 49 | Default background color | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[49mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#5D5D00;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>World</span></span></pre> |
| 58 | Set underline color (58;5;n or 58;2;r;g;b)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[4mHi <span style="color:#EE0000">\x1B</span>[58;2;110;120;170mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px solid'>Hi </span><span style='text-decoration:underline 1px solid #6E78AA'>World</span></pre> |
| 59 | Default underline color | <code><span style="color:#A31515">"<span style="color:#EE0000">\x1B</span>[44;33;1;2;3;4;9;58;5;1mHi <span style="color:#EE0000">\x1B</span>[59mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through'><span style='text-decoration:underline 1px solid #BB0000'>Hi </span></span><span style='color:#5D5D00;background:#0000BB;font-weight:900;font-style:italic;text-decoration:line-through underline 1px solid'>World</span></pre> |
| 90–97 | Set foreground color (bright)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[92mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:#00FF00'>World</span></pre> |
| 100–107 | Set background color (bright)<br/>{{link_to 'ansi_colors_sequences' 'more details'}} | <code><span style="color:#A31515">"Hi <span style="color:#EE0000">\x1B</span>[102mWorld"</span></code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='background:#00FF00'>World</span></pre> |