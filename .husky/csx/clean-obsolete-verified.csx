using System.Diagnostics;


var rootFolder = Directory.GetCurrentDirectory();

string GetGitStatus()
{
    var startInfo = new ProcessStartInfo
    {
        FileName = "git",
        Arguments = "status --porcelain",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true,
    };

    using (var process = new Process { StartInfo = startInfo })
    {
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return result;
    }
}

var gitStatus = GetGitStatus();
if(!string.IsNullOrEmpty(gitStatus))
{
    Console.WriteLine("❌ \x1b[31mNot in a clean Git state.\x1b[0m");
    Console.WriteLine(gitStatus);
    return 1;
}

var expectationsFolder = Path.Combine(rootFolder, "doc", "tests_expectations");
Console.WriteLine($"➡️ delete all '.verified.' files in {expectationsFolder}");
foreach(var file in Directory.GetFiles(expectationsFolder, "*.verified.*"))
{
    File.Delete(file);
    Console.WriteLine($"Deleted: {file}");
}

Console.WriteLine($"➡️ dotnet test");
Process.Start("dotnet", "test").WaitForExit();
Console.WriteLine($"➡️ dotnet verify accept -y");
Process.Start("dotnet", "verify accept -y").WaitForExit();

gitStatus = GetGitStatus();
if(!string.IsNullOrEmpty(gitStatus))
{
    Console.WriteLine("❌ \x1b[31mObsolete '.verified.' files found.\x1b[0m");
    Console.WriteLine(gitStatus);
    return 1;
}

Console.WriteLine("✅ \x1b[32mNo obsolete '.verified.' files found.\x1b[0m");
return 0;
