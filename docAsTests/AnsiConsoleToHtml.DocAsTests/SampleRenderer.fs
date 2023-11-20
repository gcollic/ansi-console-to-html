module SampleRenderer

open AnsiConsoleToHtml

let renderSample sample =
    let result = AnsiConsole.ToHtml sample

    let dotnet =
        $"{nameof AnsiConsole}.{nameof AnsiConsole.ToHtml}(@{Colorizer.toDotNetString sample})"
        |> Colorizer.cSharp

    let bash = $"<pre>printf {Colorizer.toUnixShellString sample}</pre>"

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
{result}
  </div>
  <div role="tabpanel" hidden>
{dotnet}
  </div>
  <div role="tabpanel" hidden>
{bash}
  </div>
</div>
"""
