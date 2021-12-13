using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public class Day1 : SolutionBase<int, int>
    {
        private List<int> _data;

        public Day1(ResultParsers resPar) : base(resPar.IntToString, resPar.IntToString)
        {
        }

        public override int Day => 1;
        public override int Year => 2021;

        public override async Task Init() => _data = await InputSource.GetInputForDay(1, str =>
        {
            var toReturn = new List<int>();
            foreach (var numStr in str.Trim().Split('\n'))
            {
                if (int.TryParse(numStr, out int num))
                    toReturn.Add(num);
                else
                    throw new ArgumentException(numStr + " could not be parsed");
            }

            return toReturn;
        });

        protected override int PartOneImplementation() => NumOfSumLargerThanPrevious(_data);

        protected override int PartTwoImplementation()
        {
            var triplets = new Dictionary<int, int>();
            
            for (int i = 0; i < _data.Count-2; i++)
            {
                var current = _data[i];
                var next = _data[i+1];
                var nextNext = _data[i+2];

                triplets.Add(i, current + next + nextNext);
            }
            return NumOfSumLargerThanPrevious(triplets.Values);
        }

        private int NumOfSumLargerThanPrevious(IEnumerable<int> nums)
        {
            var res = nums.Aggregate(new {
                Previous = 0,
                Depth = 0       
            }, (data, current) =>
            {
                var depth = data.Depth;

                if(data.Previous != 0 && data.Previous < current)
                    depth++;

                return new {
                    Previous = current,
                    Depth  = depth
                };
            });

            return res.Depth;
        }
    }
}
