require "rake/clean"
require "benchmark"

TESTS = File.expand_path "tests.txt"

SOLUTIONS = [
	{
		:dir => "go",
		:build => "go build Palindromes.go",
		:run => "./Palindromes"
	},
	{
		:dir => "fsharp",
		:build => "fsharpc Palindromes.fs",
		:run => "mono-sgen Palindromes.exe"
	},
	{
		:dir => "haskell",
		:build => "ghc --make -O2 Palindromes.hs",
		:run => "./Palindromes"
	},
]

task :default => :run

SOLUTIONS.each do |solution|
	lang = solution[:dir]
	namespace :build do
		desc "Build #{lang} solution"
		task lang do
			Dir.chdir(lang) do
				sh solution[:build]
			end
		end
	end
	namespace :run do
		desc "Run #{lang} solution"
		task lang => "build:#{lang}" do
			Dir.chdir(lang) do
				sh "cat #{TESTS} | #{solution[:run]}"
			end
		end
	end
	desc "Run all solutions"
	task :run => "run:#{lang}"
	desc "Build all solutions"
	task :build => "build:#{lang}"
end

