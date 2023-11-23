
| n | Description | Example | Rendered |
|---|-------------|---------|----------|
| 0 | Reset | <code style='color:#A31515;'>"\x1B[32mHi \x1B[0mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='color:#00BB00;'>Hi </span>World</pre> |
| 1 | Bold or intense | <code style='color:#A31515;'>"Hi \x1B[1mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='font-weight:900;'>World</span></pre> |
| 3 | Italic | <code style='color:#A31515;'>"Hi \x1B[3mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='font-style:italic;'>World</span></pre> |
| 23 | Not italic | <code style='color:#A31515;'>"\x1B[3mHi \x1B[23mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'><span style='font-style:italic;'>Hi </span>World</pre> |
| 30–37 | Set foreground color | <code style='color:#A31515;'>"Hi \x1B[32mWorld"</code> | <pre style='color:#FFFFFF;background:#000000'>Hi <span style='color:#00BB00;'>World</span></pre> |
