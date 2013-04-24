import Data.Int
import Control.Monad

palindrome n = n == go 0 n
  where go rev 0 = rev
        go rev acc = go (rev * 10 + mod acc 10) (div acc 10)

palindromes_squares_between low high = filter accept [start..end]
  where sq = sqrt . fromIntegral
        (start, end) = (ceiling $ sq low, floor $ sq high)
        accept n = palindrome n && palindrome (n ^ 2)

run test = length $ palindromes_squares_between low high
  where [low, high] = map read (words test) :: [Int64]

main = do
  count : tests <- fmap lines getContents
  mapM_ (print . run) tests
