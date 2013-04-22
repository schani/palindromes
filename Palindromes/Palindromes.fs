module Palindromes

let width n =
    if n = 0L then 1
    else 1 + (double n |> log10 |> int)

let palindrome_string n =
    let s = n.ToString().ToCharArray()
    s = Array.rev s

let palindrome_func n =
    let digits n =
        let next (width, n) =
            if width = 0 then None
            else Some (n % 10L, (width - 1, n / 10L))
        Seq.unfold next (width n, n)

    let ds = digits n |> Seq.toList
    ds = List.rev ds

let palindrome_arith n =
    let rec go num reversed =
        if num = 0L then
            n = reversed
        else
            let dig  = num % 10L
            go (num / 10L) (reversed * 10L + dig)
    go n 0L

let palindromes_between (lower: int64) (upper: int64) is_palindrome =
    seq { lower .. upper }
    |> Seq.filter is_palindrome

let palindromes_squares_between (lower: int64) (upper: int64) is_palindrome =
    let sq = double >> sqrt >> int64
    palindromes_between (sq lower) (sq upper) is_palindrome
    |> Seq.filter (fun n -> pown n 2 |> is_palindrome)