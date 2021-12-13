using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    internal class Day2 : SolutionBase<int, int>
    {
        private IEnumerable<DirDto> _data;

        public Day2(ResultParsers res) : base(res.IntToString, res.IntToString)
        {
            
        }

        public override int Day => 2;

        public override int Year => 2021;

        public override async Task Init()
        {
            _data = await InputSource.GetInputForDay(Day, str => str.Trim().Split('\n').Select(line =>
            {
                var lineSplit = line.Split(" ");

                if (Enum.TryParse<Directions>(lineSplit[0], true, out var dir) &&
                    int.TryParse(lineSplit[1], out var length))
                    return new DirDto
                    {
                        Direction = dir,
                        Dist = length
                    };
                else
                    throw new ArgumentException("could not parse line: " + line);
            }));
        }

        protected override int PartOneImplementation()
        {
            const string x = "x";
            const string y = "y";
            var summed = _data.Aggregate(new Dictionary<string, int>()
            {
                {x,0 },
                {y,0 },
            }, (state, current) =>
            {
                switch (current.Direction)
                {
                    case Directions.Forward:
                        state[x] += current.Dist;
                        break;
                    case Directions.Up:
                        state[y] -= current.Dist;
                        break;
                    case Directions.Down:
                        state[y] += current.Dist;
                        break;
                    default:
                        break;
                }

                return state;
            });

            var xVal = summed["x"];
            var yVal = summed["y"];

            return xVal * yVal;
        }

        private enum Directions
        {
            Forward,
            Up,
            Down
        }

        private class DirDto
        {
            public Directions Direction { get; set; }
            public int Dist { get; set; }
        }
    }
}
