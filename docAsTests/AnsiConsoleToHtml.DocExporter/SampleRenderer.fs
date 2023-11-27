module SampleRenderer

open AnsiConsoleToHtml
open Sample

let renderSample (sample: Sample) =

    let dotnet =
        $"{nameof AnsiConsole}.{nameof AnsiConsole.ToHtml}(@{Colorizer.toDotNetString sample.Input})"
        |> Colorizer.cSharp

    let bash = $"<pre>printf {Colorizer.toUnixShellString sample.Input}</pre>"

    $"""
<div role="sample">
  <div role="tabselect">
    <ul>
      <li><a href="#" role="tab" aria-current="true">HTML</a></li>
      <li><a href="#" role="tab">DotNet</a></li>
      <li><a href="#" role="tab">Bash</a></li>
    </ul>
  </div>
  <div role="tabpanel">
{sample.Output}
  </div>
  <div role="tabpanel" hidden>
{dotnet}
  </div>
  <div role="tabpanel" hidden>
{bash}
  </div>
</div>
"""
    |> _.Replace("\r", "")
