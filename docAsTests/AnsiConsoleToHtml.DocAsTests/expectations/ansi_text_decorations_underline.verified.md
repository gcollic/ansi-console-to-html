
| n | Description | Example | Rendered |
|---|-------------|---------|----------|
| 4 | straight underline | <code style='color:#A31515;'>"\x1B[4mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px solid ;'>Hello World</span></pre> |
| 4:0 | no underline | <code style='color:#A31515;'>"\x1B[4mHello \x1B[4:0mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px solid ;'>Hello </span>World</pre> |
| 4:1 | straight underline | <code style='color:#A31515;'>"\x1B[4:1mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px solid ;'>Hello World</span></pre> |
| 4:2 | double underline | <code style='color:#A31515;'>"\x1B[4:2mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px double ;'>Hello World</span></pre> |
| 4:3 | curly underline | <code style='color:#A31515;'>"\x1B[4:3mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px wavy ;'>Hello World</span></pre> |
| 4:4 | dotted underline | <code style='color:#A31515;'>"\x1B[4:4mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px dotted ;'>Hello World</span></pre> |
| 4:5 | dashed underline | <code style='color:#A31515;'>"\x1B[4:5mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px dashed ;'>Hello World</span></pre> |
| 21 | double underline | <code style='color:#A31515;'>"\x1B[21mHello World"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px double ;'>Hello World</span></pre> |
| 24 | no underline | <code style='color:#A31515;'>"\x1B[4mHello \x1B[24mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='text-decoration:underline 1px solid ;'>Hello </span>World</pre> |