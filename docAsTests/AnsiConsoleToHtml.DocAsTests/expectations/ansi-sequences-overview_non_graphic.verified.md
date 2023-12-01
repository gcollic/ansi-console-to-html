All the following sequences are rendered as <pre style='color:#FFFFFF;background:#000000'>
Hello  World
</pre>

| Input | Sequence meaning |
|-------|------------------|
| <code><span style="color:#A31515">"Hello <span style="color:#EE0000">\x1B</span>[3A World"</span></code> | Cursor 3 times up |
| <code><span style="color:#A31515">"Hello <span style="color:#EE0000">\x1B</span>[22;5H World"</span></code> | Moves the cursor to row 22, column 5 |
| <code><span style="color:#A31515">"Hello <span style="color:#EE0000">\x1B</span>[2J World"</span></code> | Clear entire screen |
| <code><span style="color:#A31515">"Hello <span style="color:#EE0000">\x1B</span> World"</span></code> | Invalid/unfinished sequence |
