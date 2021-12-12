using System;

namespace AoC.Library.Interfaces
{
    public interface ISolution
    {
        int Day { get; }
        int Year { get; }

        string SolvePartOne();
        string SolvePartTwo();
    }
}
