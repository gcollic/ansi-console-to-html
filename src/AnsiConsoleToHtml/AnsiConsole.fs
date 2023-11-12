namespace AnsiConsoleToHtml

module AnsiConsole =
    let Colors256 () = Colors256.Colors256()

    let ToHtml (input: string) =
        "<pre style='color:#FFFFFF;background:#000000'>\n"
        + input.Replace("\x1B[32m", "<span style='color:#00BB00;'>")
        + "</span>\n</pre>"
