package main

import (
	"fmt"
	"math"
)

func palindrome(num int64) bool {
	var n, reverse, dig int64
	n = num
	reverse = 0
	for num > 0 {
		dig = num % 10
		reverse = reverse*10 + dig
		num = num / 10
	}
	return n == reverse
}

func main() {
	var cases int
	fmt.Scan(&cases)
	for i := 0; i < cases; i++ {
		var found, start, finish, sqrt_start, sqrt_finish, square int64
		fmt.Scan(&start, &finish)
		sqrt_start = int64(math.Sqrt(float64(start)))
		sqrt_finish = int64(math.Sqrt(float64(finish)))
		for j := sqrt_start; j <= sqrt_finish; j++ {
			if palindrome(j) {
				square = j * j
				if palindrome(square) && square >= start && square <= finish {
					found += 1
				}
			}
		}
		fmt.Print("Case #", (i + 1), ": ", found, "\n")
	}
}
