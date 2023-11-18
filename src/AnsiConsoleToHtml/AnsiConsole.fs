namespace AnsiConsoleToHtml

module AnsiConsole =
    let Colors256 () = Colors256.Colors256()

    let ToHtml (input: string) =
        let colors = Colors256()

        Parser.parse input
        |> Interpreter.interpretCommands colors
        |> Renderer.convertStyledTextToHtml colors
