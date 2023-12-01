module SampleRenderer

open Sample

let renderSample (sample: Sample) =

    let colorizedHtml = Colorizer.html sample.Output

    let dotnet = Colorizer.toMultilineDotNetPre sample.Input

    let powerShell = Colorizer.toPowershellPre sample.Input

    let unixShell = Colorizer.toUnixShellPre sample.Input

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
{colorizedHtml}
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
