All the following sequences are rendered as <pre style='color:#FFFFFF;background:#000000'>
Hello  World
</pre>

| Input | Sequence meaning |
|-------|------------------|
| <code style='color:#A31515;'>"Hello \x1B[3A World"</code> | Cursor 3 times up |
| <code style='color:#A31515;'>"Hello \x1B[22;5H World"</code> | Moves the cursor to row 22, column 5 |
| <code style='color:#A31515;'>"Hello \x1B[2J World"</code> | Clear entire screen |
| <code style='color:#A31515;'>"Hello \x1B World"</code> | Invalid/unfinished sequence |
