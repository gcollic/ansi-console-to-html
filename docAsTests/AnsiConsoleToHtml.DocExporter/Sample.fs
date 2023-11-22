module Sample

open System.Text.RegularExpressions

let private separatorRegex = new Regex(@"#+")

let private sampleRegex =
    new Regex(@"^(#+)\r?\n(?<input>.*)\r?\n\k<1>\r?\n(?<output>.*)$", RegexOptions.Singleline)

type Sample = {
    Input: string
    Output: string
} with

    member this.serialize() =
        let minSeparatorLength =
            separatorRegex.Matches(this.Input)
            |> Seq.map _.Value.Length
            |> Seq.append (Seq.singleton 2)
            |> Seq.max

        let separator = new string ('#', minSeparatorLength + 1)
        [ separator; this.Input; separator; this.Output ] |> String.concat "\n"

    static member deserialize(s: string) =
        let m = sampleRegex.Match(s)

        {
            Input = m.Groups["input"].Value
            Output = m.Groups["output"].Value
        }
