using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public class Day6 : SolutionBase<int, long>
    {
        private string[] _data;

        public Day6(ResultParsers resPar) : base(resPar.IntToString, resPar.LongToString)
        {
        }

        public override int Day => 6;

        public override int Year => 2021;

        public override async Task Init()
        {
            _data = await InputSource.GetInputForDay(Day, str => str.Trim().Split(","));
        }

        protected override int PartOneImplementation()
        {
            var fishCounter = _data.Select(f => {

                var newFish = new LanternFish(int.Parse(f));

                return newFish;

            }).ToList();

            for (int i = 0; i < 80; i++)
            {
                var newFishes = new List<LanternFish>();
                foreach (var fish in fishCounter)
                {
                    fish.SpendDay(out var newFish);
                    if (newFish != null)
                        newFishes.Add(newFish);
                }

                fishCounter.AddRange(newFishes);
            }

            return fishCounter.Count();
            //var allFish = new List<LanternFish>();
            //var fishCounter = _data.Select(f => {

            //    var newFish = new LanternFish(allFish, int.Parse(f));

            //    allFish.Add(newFish);
            //    return newFish;

            //}).ToArray();

            //for (int i = 0; i < 80; i++)
            //{
            //    foreach (var fish in fishCounter)
            //        fish.SpendDay();

            //    fishCounter = allFish.ToArray();
            //}

            //return allFish.Count();
        }

        protected override long PartTwoImplementation()
        {
            var fishCounter = _data.Select(f => {

                var newFish = new LanternFish(int.Parse(f));

                return newFish;

            }).ToList();

            for (int i = 0; i < 256; i++)
            {
                var newFishes = new List<LanternFish>();
                foreach (var fish in fishCounter)
                {
                    fish.SpendDay(out var newFish);
                    if(newFish != null)
                        newFishes.Add(newFish);
                }

                fishCounter.AddRange(newFishes);
            }

            return fishCounter.Count();
        }

        class LanternFish
        {
            private const int DaysToSpawn = 7;

            private int _state;
            public LanternFish(int initialState = 8)
            {
                _state = initialState;
            }

            public void SpendDay(out LanternFish newFish)
            {
                newFish = null;
                if (_state == 0)
                {
                    newFish = new LanternFish();
                    _state = DaysToSpawn;
                }

                _state -= 1;
            }
        }
    }
}
