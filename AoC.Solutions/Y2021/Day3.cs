using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public class Day3 : SolutionBase<int, int>
    {
        private string[] _data;

        public Day3(ResultParsers resPar) : base(resPar.IntToString, resPar.IntToString)
        {
        }

        public override int Day => 3;

        public override int Year => 2021;

        public override async Task Init() => _data = await InputSource.GetInputForDay(Day, str => str.Trim().Split('\n'));

        protected override int PartOneImplementation()
        {
            var numLines = _data.Length;
            var bin = "";
            var binVerted = "";
            var bins = GetCountOfBitsInPositions();

            foreach (var dataPoint in bins)
            { //there should be an easier way
                bin += dataPoint > numLines / 2 ? "1" : "0";
                binVerted += dataPoint > numLines / 2 ? "0" : "1";
            }

            return Convert.ToInt32(bin, 2) * Convert.ToInt32(binVerted, 2);
        }

        private int[] GetCountOfBitsInPositions()
        {
            var binResult = new int[_data[0].Length];
            foreach (var line in _data)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (int.TryParse(line[i].ToString(), out var binNum))
                        binResult[i] += binNum;
                }
            }

            return binResult;
        }

        protected override int PartTwoImplementation()
        {
            var generatorRating = GetRatingRecursive("1", _data);
            var oxygenRating = GetRatingRecursive("0", _data);

            return Convert.ToInt32(generatorRating, 2) * Convert.ToInt32(oxygenRating, 2);
        }

        /// <summary>
        /// Finds the rating for a given preference
        /// </summary>
        /// <param name="preference">Preference, string 0, or string 1. 0 C02 Scrubber rating, 1 being Oxygen Generator rating</param>
        /// <param name="lookIn">the array of strings to look through</param>
        /// <param name="startPos">Current Position, default 0, used only in recursion</param>
        /// <returns>The fit</returns>
        private string GetRatingRecursive(string preference, IEnumerable<string> lookIn, int startPos = 0)
        {
            if (lookIn.Count() == 1)
                return lookIn.First();

            var preferenceItems = new List<string>();
            var nonpreferenceItems = new List<string>();

            foreach (var item in lookIn)
            {
                var lookAt = item[startPos].ToString();

                if(lookAt.Equals(preference))
                    preferenceItems.Add(item);
                else
                    nonpreferenceItems.Add(item);
            }

            if (preferenceItems.Count() == nonpreferenceItems.Count())
                return GetRatingRecursive(preference, preferenceItems, startPos + 1);

            if(preference == "0")
                return GetRatingRecursive(preference, preferenceItems.Count() < nonpreferenceItems.Count() ? preferenceItems : nonpreferenceItems, startPos + 1);

            return GetRatingRecursive(preference, preferenceItems.Count() > nonpreferenceItems.Count() ? preferenceItems : nonpreferenceItems, startPos + 1);
        }
    }
}
