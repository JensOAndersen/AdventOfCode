using AoC.Library.Interfaces;
using System;
using System.Threading.Tasks;

namespace AoC.Library.Abstractions
{

    public abstract class SolutionBase<TResultPartOne, TResultPartTwo> : ISolution
    {
        private readonly Func<TResultPartOne, string> _resultToStringPartOne;
        private readonly Func<TResultPartTwo, string> _resultToStringPartTwo;

        public abstract int Day { get; }
        public abstract int Year { get; }

        protected SolutionBase(Func<TResultPartOne, string> resultToStringPartOne, Func<TResultPartTwo, string> resultToStringPartTwo)
        {
            _resultToStringPartOne = resultToStringPartOne;
            _resultToStringPartTwo = resultToStringPartTwo;
        }

        public string SolvePartOne()
        {
            var solveTime = SolvePartTimed(PartOneImplementation, out var result);

            return $"Task 1 solved in {solveTime.Milliseconds} milliseconds, the result is: '{_resultToStringPartOne(result)}'";
        }
        public string SolvePartTwo()
        {
            var solveTime = SolvePartTimed(PartTwoImplementation, out var result);

            return $"Task 2 solved in {solveTime.Milliseconds} milliseconds, the result is: '{_resultToStringPartTwo(result)}'";
        }

        private TimeSpan SolvePartTimed<TResult>(Func<TResult> solveFunc, out TResult result)
        {
            var dateThen = DateTime.Now;

            result = solveFunc();

            var dateNow = DateTime.Now;
            return dateNow - dateThen;
        }
        protected virtual TResultPartOne PartOneImplementation() => default;
        protected virtual TResultPartTwo PartTwoImplementation() => default;
        public abstract Task Init();
    }
}
