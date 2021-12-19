using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public class Day8 : SolutionBase<int, int>
    {
        private Day8Dto[] _data;

        public Day8(ResultParsers resPar) : base(resPar.IntToString, resPar.IntToString)
        {
        }

        public override int Day => 8;

        public override int Year => 2021;

        public override async Task Init()
        {
            _data = (await InputSource.GetInputForDay(Day, str => str.Trim().Split("\n").Select(line =>
            {
                var split = line.Split("|").Select(st => st.Trim().Split(" ")).ToArray();

                return new Day8Dto
                {
                    Numbers = split[0],
                    Instructions = split[1]
                };
            }))).ToArray();
        }

        protected override int PartOneImplementation() => _data.SelectMany(dto => dto.Instructions.Where(inst => inst.Length == 2 || inst.Length == 4 || inst.Length == 3 || inst.Length == 7)).Count();

        protected override int PartTwoImplementation() => _data.Sum(x => x.ResolveToNumber());

        class Day8Dto
        {
            public string[] Numbers { get; set; }
            public string[] Instructions { get; set; }

            public int ResolveToNumber()
            {
                var pos1 = ""; //topmost
                var pos2 = ""; //top left
                var pos3 = ""; //top right
                var pos4 = ""; //middle
                var pos5 = ""; //bottom left
                var pos6 = ""; //bottom right
                var pos7 = ""; //bottommost

                var instructionArrays = GetFullListOfNumbersInSet();

                /*
                 * count of letters in instruction:
                 * 2 : 1
                 * 3 : 7
                 * 4 : 4
                 * 5 : 2, 3, 5
                 * 6 : 0, 6, 9
                 * 7 : 8
                 */

                /*
                 * rules for letters
                 * 0 : The only instruction missing pos4
                 * 1 : only instruction with length 2
                 * 2 : 
                 * 3 : 
                 * 4 : only instruction with length 4
                 * 5 : 
                 * 6 :
                 * 7 : only instruction with length 3
                 * 8 : only instruction with length 7
                 * 9 : 
                 */

                /*
                 * pos4 is all instructions with 6 elements where the letter only occurs once (0, 6, 9)
                 * pos4 is locked
                 * 
                 */

                //resolve numbers to integers

                return 0;
            }

            private string[] GetFullListOfNumbersInSet()
            {
                var res = Numbers.Concat(Instructions).Select(str =>
                {
                    var arr = str.ToCharArray();
                    Array.Sort(arr);
                    return string.Join(string.Empty, arr);
                }).Distinct().ToArray();

                return res;
            }
        }
    }
}
