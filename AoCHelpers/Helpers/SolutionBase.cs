using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelpers.Helpers
{
    public abstract class SolutionBase
    {
        public string SolvePart<TResult>(SolvePartEnum solvePartEnum, Func<TResult, string> resultToString)
        {
            var dateThen = DateTime.Now;
            TResult result = default(TResult);

            switch (solvePartEnum)
            {
                case SolvePartEnum.PartOne:
                    result = PartOneImplementation<TResult>();
                    break;
                case SolvePartEnum.PartTwo:
                    result = PartTwoImplementation<TResult>();
                    break;
                default:
                    break;
            }
            var dateNow = DateTime.Now;
            var dateDiff = dateNow - dateThen;

            return $"Task solved in {dateDiff.TotalMilliseconds} miliseconds, the result is: {resultToString(result)}";
        }

        public abstract TResult PartOneImplementation<TResult>();
        public abstract TResult PartTwoImplementation<TResult>();
    }
}
