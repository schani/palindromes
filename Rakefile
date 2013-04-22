require "rake/clean"

CLEAN.include "*.exe"

task :default => :run

task :build do
	sh "fsharpc Palindromes/Program.fs -o palindrome.exe"
end

task :run => :build do
	sh "mono-sgen palindrome.exe tests.txt"
end
