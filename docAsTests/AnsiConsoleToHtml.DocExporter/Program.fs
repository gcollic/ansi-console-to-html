// For more information see https://aka.ms/fsharp-console-apps
open System
open System.Linq

let showCommandLineArgs () =
    Environment.GetCommandLineArgs()
    |> Array.iteri (fun index arg -> printfn $"arg[{index}] = {arg}")

printfn "Hello World"
showCommandLineArgs ()
exit 0
