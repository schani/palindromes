module Util

open System
open System.IO
open System.Net
open System.Linq
open System.Security.Cryptography

let flip f x y = f y x

let always x _ = x

let head, map, iter, filter, exists, collect, tryFind, choose, rev =
    List.head, List.map, List.iter, List.filter, List.exists, List.collect, List.tryFind, List.choose, List.rev

let tail = List.tail

let elem x xs = exists ((=) x) xs

let reject f = filter (f >> not)

let uniq xs = xs |> Set.ofList |> Set.toList

let show (x: obj) = sprintf "%A" x
let print (x: obj) = printfn "%A" x

let curl (url: string) =
    let client = new WebClient ()
    client.DownloadString (url)

let wget (url: string) (file: string) =
    let client = new WebClient ()

    let dlFormat = "Downloaded: {0}% ({1:N2}MiB / {2:N2}MiB)"
    Console.WriteLine (dlFormat, 0, 0, 0)

    let wait = new System.Threading.ManualResetEvent (false)
    let sync = new Object()

    client.DownloadFileCompleted.Add (fun _ -> wait.Set() |> ignore)
    client.DownloadProgressChanged.Add (fun args ->
        lock sync (fun () ->
            Console.SetCursorPosition (0, Console.CursorTop - 1)
            let mebibyte = pown 2.0 20
            Console.WriteLine (dlFormat, args.ProgressPercentage, (float)args.BytesReceived / mebibyte, (float)args.TotalBytesToReceive / mebibyte))
        )

    client.DownloadFileAsync (new Uri (url), file)

    wait.WaitOne()
    file

let lines (s: string) =
    s.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)
    |> Array.toList

let unlines: string seq -> string =
    String.concat "\n"

let words (s: string) =
    // TODO make this better account for tabs and other whitespace
    s.Split([|' '|])
    |> Array.toList

let unwords: string seq -> string =
    String.concat " "

let lookup x xs = 
    xs |> List.tryFind (fun (a, b) -> a = x) |> Option.map snd

let md5 (file : string) =
    use reader = new StreamReader(file)
    let hasher = new MD5CryptoServiceProvider ()
    reader.BaseStream |> hasher.ComputeHash |> BitConverter.ToString

// from http://bradclow.blogspot.com/2009/07/f-option-orelse-getorelse-functions.html
let orElse o (p: 'a option) = if Option.isSome o then o else p
let (|?) = orElse
let (|?|) = defaultArg

module Option =
    let rec concat = function
        | Some x :: rest -> x :: concat rest
        | None :: rest -> concat rest
        | [] -> []

type Directory with
    static member Copy (source, dest) =
        for dir in Directory.GetDirectories(source, "*", SearchOption.AllDirectories) do
            Directory.CreateDirectory(dir.Replace(source, dest))
        for file in Directory.GetFiles(source, "*", SearchOption.AllDirectories) do
            File.Copy(file, file.Replace(source, dest))
        dest

type Path with
    static member Home =
        match Environment.OSVersion.Platform with
        | PlatformID.MacOSX -> Environment.GetEnvironmentVariable "HOME"
        | _ -> Environment.GetFolderPath Environment.SpecialFolder.Personal

    static member Expand (path: string) = path.Replace ("~", Path.Home)
