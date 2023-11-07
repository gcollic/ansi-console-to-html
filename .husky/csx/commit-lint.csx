/// <summary>
/// a simple regex commit linter example
/// https://www.conventionalcommits.org/en/v1.0.0/
/// https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#type
/// </summary>

using System.Text.RegularExpressions;


private var pattern = @"^(?=.{1,90}$)(?:build|feat|ci|chore|docs|fix|perf|refactor|revert|style|test)(?:\(.+\))*(?::).{4,}(?:#\d+)*(?<![\.\s])$";
private var msg = File.ReadAllLines(Args[0])[0];

if (Regex.IsMatch(msg, pattern))
   return 0;

Console.WriteLine("❌ \x1b[31mInvalid commit message\x1b[0m");
Console.WriteLine("✅ e.g: '\x1b[32mfeat(scope): subject\x1b[0m' or '\x1b[32mfix: subject\x1b[0m'");
Console.WriteLine("   -> build feat ci chore docs fix perf refactor revert style test");
Console.WriteLine("more info: \x1b[34mhttps://www.conventionalcommits.org/en/v1.0.0/\x1b[0m");

return 1;
