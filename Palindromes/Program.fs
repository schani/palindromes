module Main

open System
open System.IO

open Util
open Palindromes

[<EntryPoint>]
let main args =
    for case in File.ReadLines ("../../../tests.txt") |> Seq.skip 1 do
        let [upper; lower] = words case |> map int64
        printf "%A %A " upper lower
        let count = palindromes_squares_between upper lower palindrome_arith |> Seq.length
        printfn "%d" count
    0