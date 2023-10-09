namespace AnsiConsoleToHtml

[<Struct>]
type Color = {
    R: byte
    G: byte
    B: byte
} with

    member this.AsHexColor() = $"#{this.R:X2}{this.G:X2}{this.B:X2}"

module Colors256 =
    let Table () =
        seq {
            yield { R = 0uy; G = 0uy; B = 0uy }
            yield { R = 187uy; G = 0uy; B = 0uy }
            yield { R = 0uy; G = 187uy; B = 0uy }
            yield { R = 187uy; G = 187uy; B = 0uy }
            yield { R = 0uy; G = 0uy; B = 187uy }
            yield { R = 187uy; G = 0uy; B = 187uy }
            yield { R = 0uy; G = 187uy; B = 187uy }
            yield { R = 187uy; G = 187uy; B = 187uy }
            yield { R = 85uy; G = 85uy; B = 85uy }
            yield { R = 255uy; G = 85uy; B = 85uy }
            yield { R = 0uy; G = 255uy; B = 0uy }
            yield { R = 255uy; G = 255uy; B = 85uy }
            yield { R = 85uy; G = 85uy; B = 255uy }
            yield { R = 255uy; G = 85uy; B = 255uy }
            yield { R = 85uy; G = 255uy; B = 255uy }
            yield { R = 255uy; G = 255uy; B = 255uy }
            let levels = [ 0uy; 95uy; 135uy; 175uy; 215uy; 255uy ]

            for r in 0..5 do
                for g in 0..5 do
                    for b in 0..5 ->
                        {
                            R = levels[r]
                            G = levels[g]
                            B = levels[b]
                        }

            for i in 0uy .. 23uy ->
                let level = 8uy + 10uy * i
                { R = level; G = level; B = level }
        }
        |> Seq.toArray
