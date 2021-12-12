using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public class Day1 : SolutionBase<int, object>
    {
        private InputSourceBase _inputSource;
        private List<int> _data;

        public Day1(ResultParsers resPar) : base(resPar.IntToString, resPar.Def)
        {
            _inputSource = InputSourceBase.GetInstance(2021);
        }

        public override int Day => 1;
        public override int Year => 2021;

        public override async Task Init() => _data = await _inputSource.GetInputForDay(1, str =>
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

        protected override int PartOneImplementation()
        {
            var res = _data.Aggregate(new {
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

        protected override object PartTwoImplementation()
        {
            return 1;
        }
    }
}
