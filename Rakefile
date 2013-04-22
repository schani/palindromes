require "rake/clean"

CLEAN.include "*.exe"

task :default => :run

task :build do
	puts "* Building F# solution..."
	sh "fsharpc Palindromes/Program.fs -o palindrome.exe"
end

task :run => :build do
	sh "time mono-sgen palindrome.exe tests.txt"
end
