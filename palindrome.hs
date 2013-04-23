import Data.Int
import Control.Monad

is_palindrome n = n == go n 0
  where go 0 rev = rev
        go acc rev = go (div acc 10) (rev * 10 + mod acc 10)

palindromes_between low high = filter is_palindrome [low..high]

palindromes_squares_between low high = filter squares $ palindromes_between low' high'
  where sq = sqrt . fromIntegral
        low' = ceiling (sq low)
        high' = floor (sq high)
        squares x = is_palindrome (x ^ 2)

run test = length $ palindromes_squares_between low high
	where [low, high] = map read (words test) :: [Int64]

main = do
  count : tests <- fmap lines (readFile "tests.txt")
  mapM_ (print . run) tests
