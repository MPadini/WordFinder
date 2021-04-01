using System;
using System.Collections.Generic;
using WordFinder.Extensions;
using System.Linq;

namespace WordFinder
{
    public class WordFinder
    {
        private char[,] twoDimentionalArray;
        private readonly int matrixMaxSize = 64;
        private readonly int topWords = 10;
        private readonly string matrixMaxSizeError = "Invalid matrix size";

        public static WordFinder GetInstance(IEnumerable<string> matrix) {
            return new WordFinder(matrix);
        }

        public WordFinder(IEnumerable<string> matrix) {
            if (!ValidateMatrix(matrix)) return;

            twoDimentionalArray = matrix.ToTwoDimensionalArray();

            if (!ValidateMatrix(twoDimentionalArray)) return;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream) {
            List<string> wordsFound = new List<string>();
            if (twoDimentionalArray == null) return wordsFound;

            int rowLength = twoDimentionalArray.GetLength(0);
            int colLength = twoDimentionalArray.GetLength(1);
            
            var wordstreamDistinct = wordstream.Distinct(StringComparer.CurrentCultureIgnoreCase);

            foreach (var word in wordstreamDistinct) {
                for (int row = 0; row < rowLength; row++) {
                    for (int col = 0; col < colLength; col++) {
                        if (Search(twoDimentionalArray, row, col, word, rowLength, colLength)) {
                            wordsFound.Add(word);
                        }
                    }
                }
            }

            return GetResult(wordsFound);
        }

        private IEnumerable<string> GetResult(IEnumerable<string> wordsFound) {
            if (wordsFound == null || !wordsFound.Any()) return wordsFound;

            List<string> results = new List<string>();
            var printWords = wordsFound.GroupBy(info => info)
                                    .Select(group => new { Word = group.Key, Count = group.Count()})
                                    .OrderByDescending(x => x.Count)
                                    .Take(topWords);


            Console.WriteLine(string.Format("Top {0} most repeated words:", topWords));
            foreach (var printWord in printWords) {
                Console.WriteLine(printWord.Word);
                results.Add(printWord.Word);
            }

            return results;
        }

        private bool ValidateMatrix(IEnumerable<string> matrix) {
            if (matrix == null || !matrix.Any()) {
                Console.WriteLine(matrixMaxSizeError);
                return false;
            }
               
            return true;
        }

        private bool ValidateMatrix(Char[,] twoDimentionalArray) {
            int rowLength = twoDimentionalArray.GetLength(0);
            int colLength = twoDimentionalArray.GetLength(1);

            if (rowLength > matrixMaxSize || colLength > matrixMaxSize) {
                Console.WriteLine(matrixMaxSizeError);
                return false;
            }

            return true;
        }


        private static bool Search(char[,] grid, int row, int col, String word, int rowLength, int colLength) {
            // If first character of word doesn't match
            // with given starting point in grid.
            if (grid[row, col] != word[0]) {
                return false;
            }

            int len = word.Length;
            int k;

            // First character is already checked,
            // match remaining characters
            for (k = 1; k < len; k++) {
                int c = col + k;
                // If out of bound break
                if (c >= colLength) break;

                //left to right
                if (grid[row, c] != word[k]) {
                   break;
                }

                if (k == len - 1)  return true;
            }

            for (k = 1; k < len; k++) {
                int r = row + k;
                // If out of bound break
                if (r >= rowLength) break;

                //top to bottom
                if (grid[r, col] != word[k]) {
                    break;
                }

                if (k == len - 1) return true;
            }

            return false;
        }
    }
}
