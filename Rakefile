require "rake/clean"

CLEAN.include "*.exe"
CLEAN.include "*.hi"
CLEAN.include "*.o"
CLEAN.include "palindrome-hs"
CLEAN.include "palindrome-go"

task :default => :run

task :build do
	puts "* Building F# solution..."
	sh "fsharpc Palindromes/Program.fs -o palindrome.exe"
	puts "* Building Haskell solution..."
	sh "ghc --make -O2 palindrome.hs -o palindrome-hs"
	puts "* Building Go solution..."
	sh "go build -o palindrome-go palindrome.go"
end

task :run => :build do
	sh "time mono-sgen palindrome.exe tests.txt"
	sh "time ./palindrome-hs tests.txt"
	sh "cat tests.txt | time ./palindrome-go"
end
