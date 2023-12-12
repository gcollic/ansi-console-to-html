module VerboseIo

open System.IO

type VerboseIo = private {
    rootFolder: string
} with

    member this.toRelative path =
        Path.GetRelativePath(this.rootFolder, path)

    member this.delete file =
        File.Delete file
        printfn $"🗑️ Deleted '%s{this.toRelative file}'"

    member this.copy source destination =
        File.Copy(source, destination, true)
        printfn $"✒️ Copied '%s{this.toRelative destination}'"

    member this.write fileName content =
        File.WriteAllText(fileName, content)
        printfn $"⚙️ Wrote '%s{this.toRelative fileName}'"

    static member forFolder(rootFolder: string) = { rootFolder = rootFolder }
