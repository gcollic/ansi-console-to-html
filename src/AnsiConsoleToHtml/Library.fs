namespace AnsiConsoleToHtml

open System

module Say =
    let greetings name = $"Hello {name}"
    let hello name = greetings name |> Console.WriteLine
