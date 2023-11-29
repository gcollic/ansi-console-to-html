module SampleRenderer

open AnsiConsoleToHtml
open Sample

let renderSample (sample: Sample) =

    let dotnet =
        $"{nameof AnsiConsole}.{nameof AnsiConsole.ToHtml}(@{Colorizer.toDotNetString sample.Input})"
        |> Colorizer.cSharp

    let rawHtml = Colorizer.html sample.Output

    let powerShell = $"<pre>echo {Colorizer.toPowerShellString sample.Input}</pre>"

    let unixShell = $"<pre>printf {Colorizer.toUnixShellString sample.Input}</pre>"

    $"""
<div role="sample">
  <div role="tabselect">
    <ul>
      <li><a href="#" role="tab" aria-current="true">HTML</a></li>
      <li><a href="#" role="tab">Raw HTML</a></li>
      <li><a href="#" role="tab">DotNet</a></li>
      <li><a href="#" role="tab">PowerShell</a></li>
      <li><a href="#" role="tab">Unix shell</a></li>
    </ul>
  </div>
  <div role="tabpanel">
{sample.Output}
  </div>
  <div role="tabpanel" hidden>
{rawHtml}
  </div>
  <div role="tabpanel" hidden>
{dotnet}
  </div>
  <div role="tabpanel" hidden>
{powerShell}
  </div>
  <div role="tabpanel" hidden>
{unixShell}
  </div>
</div>
"""
    |> _.Replace("\r", "")
