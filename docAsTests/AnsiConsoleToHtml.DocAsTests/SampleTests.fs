module SampleTests

open Expecto
open Sample

[<Tests>]
let tests =
    [
        ("Empty", { Input = ""; Output = "" })
        ("Nominal", { Input = "Hello"; Output = "World" })
        ("With separator",
         {
             Input = "Hello\n###\nAnother"
             Output = "Great\n###\nWorld"
         })
    ]
    |> List.map (fun (testName, subject) ->
        test testName {
            let actual = subject.serialize () |> Sample.deserialize
            Expect.equal actual subject $"[{testName}] Sample input and output should be equal"
        })
    |> testList "Sample"
