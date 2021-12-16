using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using AoC.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public partial class Day5 : SolutionBase<int, int>
    {
        private IEnumerable<Day5Dto> _data;

        public Day5(ResultParsers resPar) : base(resPar.IntToString, resPar.IntToString)
        {
        }

        public override int Day => 5;

        public override int Year => 2021;

        public override async Task Init()
        {
            _data = await InputSource.GetInputForDay(Day, str => str.Trim().Split("\n").Select(line => new Day5Dto(line)));
        }

        protected override int PartOneImplementation()
        {
            var result = _data.Where(dt => dt.IsStraight()).Aggregate(new Dictionary<Point, int>(), (state, current) =>
            {
                if (!current.IsStraight())
                    return state;

                foreach (var point in GetPointsBetweenPointsFrom(current))
                {
                    if (state.ContainsKey(point))
                        state[point]++;
                    else
                        state.Add(point, 1);
                }

                return state;
            });

            return result.Values.Where(x => x > 1).Count();
        }

        //this gives the wrong output with full input, correct with sample input
        protected override int PartTwoImplementation()
        {
            var result = _data.Aggregate(new Dictionary<Point, int>(), (state, current) =>
            {
                foreach (var point in GetPointsBetweenPointsFrom(current).ToArray())
                {
                    if (state.ContainsKey(point))
                        state[point]++;
                    else
                        state.Add(point, 1);
                }

                return state;
            });


            /*var maxY = result.Keys.Max(x => x.Y)+1;
            var maxX = result.Keys.Max(x => x.X)+1;

            var res2D = new int[maxY, maxX];

            foreach (var item in result)
            {
                res2D[item.Key.Y, item.Key.X] = item.Value;
            }

            var stringRes = "";

            for (int i = 0; i < res2D.GetLength(0); i++)
            {
                for (int j = 0; j < res2D.GetLength(1); j++)
                {
                    stringRes += res2D[i, j].ToString();
                }
                stringRes += "\n";
            }*/

            return result.Values.Where(x => x > 1).Count();
        }

        private IEnumerable<Point> GetPointsBetweenPointsFrom(Day5Dto dto)
        {
            var from = dto.From;
            var to = dto.To;

            yield return from;

            while (!from.Equals(to))
                yield return from = from.MoveCloserTo(to);
        }

        //original part one solution
        private IEnumerable<Point> GetStraightPointsBetweenPointsFrom(Day5Dto cto)
        {
            if (!cto.IsStraight())
                throw new ArgumentException();

            if (cto.IsVertical())
            {
                var lowestY = cto.From.Y < cto.To.Y ? cto.From.Y : cto.To.Y;
                var highestY = cto.From.Y > cto.To.Y ? cto.From.Y : cto.To.Y;

                for (int i = lowestY; i <= highestY; i++)
                    yield return new Point(cto.From.X, i);

            }
            else
            {
                var lowestX = cto.From.X < cto.To.X ? cto.From.X : cto.To.X;
                var highestX = cto.From.X > cto.To.X ? cto.From.X : cto.To.X;

                for (int i = lowestX; i <= highestX; i++)
                    yield return new Point(i, cto.From.Y);

            }
        }

        class Day5Dto
        {
            public Point From { get; }
            public Point To { get; }
            public Day5Dto(string line)
            {
                var fromTo = line.Split(" -> ");

                From = new Point(fromTo[0], ",");
                To = new Point(fromTo[1], ",");
            }

            public bool IsVertical() => From.X == To.X;
            public bool IsHorizontal() => From.Y == To.Y;
            public bool IsStraight() => IsVertical() || IsHorizontal();
        }
    }
}
