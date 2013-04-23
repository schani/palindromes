module Palindrome where

import Data.Int
import Control.Monad

is_palindrome :: Int64 -> Bool
is_palindrome n = n == go n 0
  where go 0 rev = rev
        go acc rev = go (div acc 10) (rev * 10 + mod acc 10)

palindromes_between low high = filter is_palindrome [low..high]

palindromes_squares_between low high = filter square_is_palindrome $ palindromes_between low' high'
  where sq = sqrt . fromIntegral
        low' = ceiling (sq low)
        high' = floor (sq high)
        square_is_palindrome x = is_palindrome (x ^ 2)

run = length . (\[x, y] -> palindromes_squares_between x y) . map read . words

main = do
  count : tests <- fmap lines (readFile "tests.txt")
  mapM_ (print . run) tests
