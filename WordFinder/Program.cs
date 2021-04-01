using System.Collections.Generic;

namespace WordFinder
{
    class Program
    {
        static void Main(string[] args) {
            var wordFinder = WordFinder.GetInstance(CreateMatrix());

            wordFinder.Find(CreateWordStream());
        }

        private static IEnumerable<string> CreateMatrix() {
            List<string> matrix = new List<string>();

            //Create your matrix
            matrix.Add("GEEKSFGRGEEKSABCDFAEDASDASGEEKSFGRGEEKSABCDFAEDASDASBCDFAEDASDAS");
            matrix.Add("GEEKSFGRFFFFFABCDFAEDASDASGEEKSFGRGEEKSABCDFAEDASDASBCDFAEDASDAS");
            matrix.Add("ABCDFAEDASDASABCDFAEDASDASGEEKSFGRGEEKSABCDFAEDOGDASBCDFAEDASDAS");
            matrix.Add("ABCDFAEDASDASABCDDOGDASDASGEEKSFGRGEEKSABCDFAEDASDASBCDFAEDASDAS");
            matrix.Add("ABCDFAKDASGEEABCDFAEDASDASGEEKSFGRGEEKSABCDFAEDASDASBCDFAEGUITAR");

            return matrix;
        }

        private static IEnumerable<string> CreateWordStream() {
            List<string> wordstream = new List<string>();

            //Add words to find
            wordstream.Add("GEEK");
            wordstream.Add("DOG");
            wordstream.Add("GUITAR");

            return wordstream;
        }
    }
}
