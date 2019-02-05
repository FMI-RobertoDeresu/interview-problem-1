using System;
using System.Collections.Generic;
using System.Linq;

namespace Problema
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = "a{b,c}";
            var output = GetResult(input);

            foreach (var outputItem in output)
                Console.WriteLine(outputItem);

            Console.ReadKey();
        }

        private static IList<string> GetResult(string input)
        {
            var firstOpenedBracket = input.IndexOf('{');
            var lastClosedBracket = input.LastIndexOf('}');

            if (firstOpenedBracket == -1 && lastClosedBracket == -1)
                return input.Split(',').ToList();

            var prefix = string.Join("", input.Take(firstOpenedBracket));
            var suffix = string.Join("", input.Reverse().Take(input.Length - lastClosedBracket - 1).Reverse());

            var innerInput = string.Join("", input.Skip(firstOpenedBracket + 1).Take(lastClosedBracket - firstOpenedBracket - 1));
            var innerParts = ParseBracketsInner(innerInput);
            var innerPartsOutput = innerParts.Select(GetResult).ToList();
            var innerOutput = innerPartsOutput.SelectMany(x => x).ToList();

            var output = new List<string>();
            foreach (var innerOutputItem in innerOutput)
            {
                var outputItem = $"{prefix}{innerOutputItem}{suffix}";
                output.Add(outputItem);
            }

            return output;
        }

        private static IList<string> ParseBracketsInner(string input)
        {
            var output = new List<string>();
            var openedBrackets = 0;
            var outputItem = string.Empty;

            foreach (var item in input)
            {
                openedBrackets += item == '{' ? 1 : item == '}' ? -1 : 0;
                if (item == ',' && openedBrackets == 0)
                {
                    output.Add(outputItem);
                    outputItem = string.Empty;
                }
                else
                    outputItem += item;
            }

            output.Add(outputItem);

            return output;
        }
    }
}