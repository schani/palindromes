require "rake/clean"

CLEAN.include "*.exe"
CLEAN.include "*.hi"
CLEAN.include "*.o"
CLEAN.include "palindrome"

task :default => :run

task :build do
	puts "* Building F# solution..."
	sh "fsharpc Palindromes/Program.fs -o palindrome.exe"
	puts "* Building Haskell solution..."
	sh "ghc --make -O2 palindrome.hs"
end

task :run => :build do
	sh "time mono-sgen palindrome.exe tests.txt"
	sh "time ./palindrome tests.txt"
end
