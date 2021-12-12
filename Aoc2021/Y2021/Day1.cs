using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;

namespace AoC.Solutions.Y2021
{
    public class Day1 : SolutionBase<int, object>
    {
        private InputSourceBase _inputSource;

        public Day1(ResultParsers resPar) : base(resPar.IntToString, resPar.Def)
        {
            _inputSource = InputSourceBase.GetInstance(2021);
        }

        public override int Day => 1;
        public override int Year => 2021;

        protected override int PartOneImplementation()
        {
            return 1;
        }

        protected override object PartTwoImplementation()
        {
            return 1;
        }
    }
}
