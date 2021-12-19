using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    internal class Day7 : SolutionBase<int, int>
    {
        private int[] _data;

        public Day7(ResultParsers resPar) : base(resPar.IntToString, resPar.IntToString)
        {
        }

        public override int Day => 7;

        public override int Year => 2021;

        public override async Task Init()
        {
            _data = (await InputSource.GetInputForDay(Day, str => str.Trim().Split(",").Select(x => int.Parse(x)))).ToArray();
        }

        protected override int PartOneImplementation()
        {
            int min = _data.Min();
            int max = _data.Max();

            var resDict = new Dictionary<int, int>();

            //brute force
            for (int i = min; i < max; i++)
                resDict.Add(i, CalculateFuelCostForPositionPartOne(i));

            var rest1 = resDict.Values.Min();

            //there is a value difference of two between the brute force and the recursive method....
            //var res = CalculatePartOneRecursively(min, max, int.MaxValue);

            return rest1;
        }


        protected override int PartTwoImplementation()
        {
            int min = _data.Min();
            int max = _data.Max();

            var resDict = new Dictionary<int, int>();

            //brute force
            for (int i = min; i < max; i++)
                resDict.Add(i, CalculateFuelCostForPositionPartTwo(i));

            var rest1 = resDict.Values.Min();

            return rest1;
        }

        //actually trying to be smart
        private int CalculatePartOneRecursively(int min, int max, int previousMin)
        {
            //calculate fuel cost for min and max values
            var minFuel = CalculateFuelCostForPositionPartOne(min);
            var maxFuel = CalculateFuelCostForPositionPartOne(max);

            //find midpoint
            var halfPoint = (max + min) / 2;

            //figure out which of the two fuel spenders use the least
            var leastFuel = minFuel < maxFuel ? minFuel : maxFuel;

            //if least is higher than previous, return previous, also if halfpoint equals min, then we're down to a difference of 1 between min and max, doing this because of integer division, otherwise it results in a stackoverflow
            if (leastFuel > previousMin || halfPoint == min)
                return leastFuel;

            if (minFuel == maxFuel)
                return maxFuel;

            //if fuel cost for min value is less than fuel cost for max value, then we continue recursion for the lower half of the points between min and halfpoint
            if (minFuel < maxFuel)
                return CalculatePartOneRecursively(min, halfPoint, leastFuel);

            //reverse for the other way around
            return CalculatePartOneRecursively(halfPoint, max, leastFuel);
        }

        private int CalculateFuelCostForPositionPartOne(int pos) => _data.Select(x => Math.Abs(pos - x)).Sum();
        private int CalculateFuelCostForPositionPartTwo(int pos) => _data.Select(x =>
        {
            var val = Math.Abs(pos - x);

            var resVal = ((int)Math.Pow(val, 2) + val) / 2;
            return resVal;
        }).Sum();
    }
}
