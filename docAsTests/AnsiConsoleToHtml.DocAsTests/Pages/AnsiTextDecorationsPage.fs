module AnsiTextDecorationsPage

open Expecto
open VerifyPages
open DocPart
open ExampleRenderer

let title = "ANSI text decorations sequences"
let slug = "ansi_text_decorations"
let underlineSlug = slug + "_underline"

let pageContent =
    $"""
# {title}

## Underline

4 (underline) and 24 (no underline) are classic codes for underlines.

21 is double-underline per ECMA-48, but instead disables bold intensity on some terminals, including in the Linux kernel's console <em>before version 4.17</em>.

More recently, options have been added to the code sequence 4.
It was originally added by Kitty, now also adopted by other terminals, and supported by applications such as vim.

See [https://sw.kovidgoyal.net/kitty/underlines/](https://sw.kovidgoyal.net/kitty/underlines/)

{{{{include '{underlineSlug}'}}}}

"""

let underlineOverview =
    [
        ("4", "straight underline", None)
        ("4:0", "no underline", Some $"\x1B[4mHello \x1B[4:0mWorld")
        ("4:1", "straight underline", None)
        ("4:2", "double underline", None)
        ("4:3", "curly underline", None)
        ("4:4", "dotted underline", None)
        ("4:5", "dashed underline", None)
        ("21", "double underline", None)
        ("24", "no underline", Some $"\x1B[4mHello \x1B[24mWorld")
    ]
    |> List.map (fun (code, description, example) ->
        (code, description, Option.defaultValue $"\x1B[{code}mHello World" example))
    |> examplesToMarkdownDocPart underlineSlug

[<Tests>]
let tests =
    verifyListOfDocPart title
    <| [
        {
            Slug = Slug.from slug
            Metadata =
                Some {
                    Title = title
                    Navbar = None
                    Toc =
                        Some {
                            Parent = "ANSI escape sequences"
                            Label = "Decorations sequences"
                            Order = 4
                        }
                }
            Content = pageContent
            Format = Markdown
        }
        underlineOverview
    ]
