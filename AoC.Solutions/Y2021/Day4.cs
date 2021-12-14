using AoC.Library.Abstractions;
using AoC.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Solutions.Y2021
{
    public class Day4 : SolutionBase<int, int>
    {
        private string[] _lineData;

        public Day4(ResultParsers resPars) : base(resPars.IntToString, resPars.IntToString)
        {
        }

        public override int Day => 4;

        public override int Year => 2021;

        public override async Task Init()
        {
            _lineData = await InputSource.GetInputForDay(Day, str => str.Trim().Split('\n'));
        }

        protected override int PartOneImplementation()
        {
            var man = new CheckerBoardManager();
            var instructions = "";
            var rowNum = 0;
            CheckerBoard currentBoard = null;
            for (int i = 0; i < _lineData.Length; i++)
            {
                if(i == 0)
                {
                    instructions = _lineData[i];
                    continue;
                }

                if (string.IsNullOrWhiteSpace(_lineData[i]))
                {
                    currentBoard = new CheckerBoard();
                    rowNum = 0;
                    continue;
                }
                else {
                    currentBoard.FeedLine(man, _lineData[i]);
                    rowNum++;
                }
            }

            foreach (var instruction in instructions.Split(','))
            {
                if(man.FeedInstruction(instruction, out var points))
                {
                    return int.Parse(instruction) * points.Select(p => int.Parse(p)).Sum();
                }
            }

            return base.PartOneImplementation();
        }

        protected override int PartTwoImplementation()
        {
            return base.PartTwoImplementation();
        }

        class CheckerBoardManager
        {
            private Dictionary<string, List<CheckerBoard>> _boards = new Dictionary<string, List<CheckerBoard>>();

            internal bool FeedInstruction(string instruction, out string[] unhitPoints)
            {
                if(_boards.TryGetValue(instruction, out var boardsWithInstruction))
                {
                    foreach (var board in boardsWithInstruction)
                    {
                        if (board.HitPoint(instruction, out var unhitPoint))
                        {
                            unhitPoints = unhitPoint;
                            return true;
                        }
                    }
                }
                unhitPoints = default;
                return false;
            }

            internal void RegisterItem(string item, CheckerBoard checkerBoard)
            {
                if (!_boards.TryGetValue(item, out var boards))
                {
                    boards = new List<CheckerBoard>();
                    _boards.Add(item, boards);
                    boards.Add(checkerBoard);
                }
                else
                    boards.Add(checkerBoard);
            }
        }

        class CheckerBoard
        {

            private Dictionary<string, int> _columns = new Dictionary<string, int>();
            private Dictionary<string, int> _rows= new Dictionary<string, int>();

            private Dictionary<int, int> _hitRows = new Dictionary<int, int>();
            private Dictionary<int, int> _hitColumns = new Dictionary<int, int>();

            public void FeedLine(CheckerBoardManager manager, string line)
            {
                var nextRow = _rows.Any() ? _rows.Max(x => x.Value) + 1 : 0;
                int iterator = 0;
                foreach (var item in line.Trim().Split(" "))
                {
                    if (string.IsNullOrWhiteSpace(item))
                        continue;
                    
                    if (!_columns.TryAdd(item, iterator))
                        throw new ArgumentException("column already contains value");

                    if(!_rows.TryAdd(item, nextRow))
                        throw new ArgumentException("Row already contains value");

                    manager.RegisterItem(item, this);

                    iterator++;
                }
            }

            public bool HitPoint(string point, out string[] unhitPoints)
            {
                if(_columns.TryGetValue(point, out var result))
                {
                    _columns.Remove(point);
                    if (_hitColumns.ContainsKey(result))
                    {
                        _hitColumns[result]++;
                        if(_hitColumns[result] == 5)
                        {
                            unhitPoints = _columns.Keys.ToArray();
                            return true;
                        }
                    }
                    else
                        _hitColumns.Add(result, 1);
                }

                if(_rows.TryGetValue(point, out result))
                {
                    _rows.Remove(point);
                    if (_hitRows.ContainsKey(result))
                    {
                        _hitRows[result]++;
                        if (_hitRows[result] == 5)
                        {
                            unhitPoints = _rows.Keys.ToArray();
                            return true;
                        }
                    }
                    else
                        _hitRows.Add(result, 1);
                }

                unhitPoints = new string[0];
                return false;
            }
        }
    }
}
