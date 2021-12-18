using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    /*
    Todo idea:
    instead of running through days n-amount of times, how about going through each fish.
    There's got to be a mathematical way of solving this, take the amount of days a fish is spawned with - remaining days for run, then multiply that and add new fish, maybe?
    Its an exponential equation, so somethingsomething^fish
     */
    public class Day6 : SolutionBase<long, long>
    {
        private string[] _data;

        public Day6(ResultParsers resPar) : base(resPar.LongToString, resPar.LongToString)
        {
        }

        public override int Day => 6;

        public override int Year => 2021;

        public override async Task Init()
        {
            _data = await InputSource.GetInputForDay(Day, str => str.Trim().Split(","));
        }

        protected override long PartOneImplementation()
        {
            var groupedData = _data.Select(x => int.Parse(x)).GroupBy(x => x).OrderBy(x => x.Key).ToDictionary(k => k.Key, v => (long)v.Count());

            var dict = NewDict();

            for (int i = 0; i < 80; i++)
            {
                foreach (var (key, val) in groupedData)
                {
                    var newKey = key - 1;
                    if (key == 0)
                    {
                        dict[8] += val;
                        dict[6] += val;
                        continue;
                    }

                    dict[newKey] += val;
                }
                groupedData = new Dictionary<int, long>(dict.ToArray());
                dict = NewDict();
            }

            return groupedData.Values.Sum();
        }

        protected override long PartTwoImplementation()
        {
            var groupedData = _data.Select(x => int.Parse(x)).GroupBy(x => x).OrderBy(x => x.Key).ToDictionary(k => k.Key, v => (long)v.Count());

            var dict = NewDict();

            for (int i = 0; i < 256; i++)
            {
                foreach (var (key, val) in groupedData)
                {
                    var newKey = key - 1;
                    if (key == 0)
                    {
                        dict[8] += val;
                        dict[6] += val;
                        continue;
                    }

                    dict[newKey] += val;
                }
                groupedData = new Dictionary<int, long>(dict.ToArray());
                dict = NewDict();
            }

            return groupedData.Values.Sum();
        }

        private Dictionary<int, long> NewDict()
        {
            var dict = new Dictionary<int, long>();
            dict.Add(0, 0);
            dict.Add(1, 0);
            dict.Add(2, 0);
            dict.Add(3, 0);
            dict.Add(4, 0);
            dict.Add(5, 0);
            dict.Add(6, 0);
            dict.Add(7, 0);
            dict.Add(8, 0);

            return dict;
        }
    }
}
