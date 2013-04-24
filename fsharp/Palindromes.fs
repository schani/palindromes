module Main

open System
open System.IO

let is_palindrome n =
    let reverse i =
        let mutable num = i
        let mutable reversed = 0L
        while num <> 0L do
            let dig  = num % 10L
            num <- num / 10L
            reversed <- reversed * 10L + dig
        reversed
    n = reverse n

let palindromes_between (lower: int64) (upper: int64) =
    seq { lower .. upper }
    |> Seq.filter is_palindrome

let palindromes_squares_between (lower: int64) (upper: int64) =
    let sq = double >> sqrt >> int64
    palindromes_between (sq lower) (sq upper)
    |> Seq.filter (fun n -> pown n 2 |> is_palindrome)

[<EntryPoint>]
let main args =
    let total = Console.ReadLine ()
    let mutable test = Console.ReadLine ()
    while test <> null do
        let [|upper; lower|] = test.Split([|' '|]) |> Array.map int64
        let count = palindromes_squares_between upper lower |> Seq.length
        printfn "%d" count
        test <- Console.ReadLine ()
    0
