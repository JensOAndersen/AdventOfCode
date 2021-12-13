using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var binResult = new int[_data[0].Length];
            foreach (var line in _data)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (int.TryParse(line[i].ToString(), out var binNum))
                        binResult[i] += binNum;
                }
            }

            var bin = "";
            var binVerted = "";
            foreach (var dataPoint in binResult)
            { //there should be an easier way
                bin += dataPoint > numLines / 2 ? "1" : "0";
                binVerted += dataPoint > numLines / 2 ? "0" : "1";
            }

            return Convert.ToInt32(bin, 2) * Convert.ToInt32(binVerted, 2);
        }
    }
}
