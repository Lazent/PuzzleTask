using System;
using System.Collections.Generic;
using System.IO;

namespace PuzzleTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Puzzle Task");

            string filePath = @"../../../source.txt";

            try
            {
                List<string> fragments = new List<string>(File.ReadAllLines(filePath));

                fragments.RemoveAll(string.IsNullOrWhiteSpace);

                string longestChain = BuildLongestChain(fragments);

                Console.WriteLine(longestChain);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something wrong: {ex.Message}");
            }

            
        }

        static string BuildLongestChain(List<string> fragments)
        {
            string longestChain = string.Empty;

            foreach (string startFragment in fragments)
            {
                var remainingFragments = new List<string>(fragments);
                remainingFragments.Remove(startFragment);

                string currentChain = startFragment;
                string lastFragment = startFragment;

                while (true)
                {
                    string nextFragment = FindNextFragment(lastFragment, remainingFragments);

                    if (nextFragment == null)
                    {
                        break;
                    }

                    currentChain += nextFragment.Substring(2);
                    lastFragment = nextFragment;
                    remainingFragments.Remove(nextFragment);
                }

                if (currentChain.Length > longestChain.Length)
                {
                    longestChain = currentChain;
                }

                
            }
            return longestChain;
        }

        static string FindNextFragment(string currentFragment, List<string> fragments)
        {
            string lastTwoDigits = currentFragment.Substring(currentFragment.Length - 2);

            foreach (string fragment in fragments)
            {
                if (fragment.StartsWith(lastTwoDigits))
                {
                    return fragment;
                }
            }

            return null;
        }

    }
}
