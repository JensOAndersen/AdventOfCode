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
    I found this to be an interesting task for structuring date.

    My idea for solving it, was to organize the data into classes which each have a dictionary of what values goes into which rows and colums,
    then having a 'hit point' method to track which rows have a certain amount of hits
     */
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
            var pair = BuildManagerAndInstructions();

            foreach (var instruction in pair.instructions.Split(','))
            {
                if (pair.manager.FeedInstruction(instruction, out var points))
                {
                    return int.Parse(instruction) * points.Select(p => int.Parse(p)).Sum();
                }
            }

            return base.PartOneImplementation();
        }

        protected override int PartTwoImplementation()
        {
            var pair = BuildManagerAndInstructions();

            var lastCompleted = new string[0];
            var lastInstruction = "";
            var instructHashSet = pair.instructions.Split(',').Select(x => int.Parse(x)).ToHashSet();

            foreach (var instruction in pair.instructions.Split(','))
            {
                lastInstruction = instruction;
                if (pair.manager.FeedInstruction(instruction, out var points))
                {
                    if (pair.manager.RemainingBoards == 0)
                    {
                        lastCompleted = points;
                        break;
                    }
                }
            }

            return int.Parse(lastInstruction) * lastCompleted.Select(p => int.Parse(p)).Sum();
        }

        private (CheckerBoardManager manager, string instructions) BuildManagerAndInstructions()
        {
            var man = new CheckerBoardManager();
            var instructions = "";
            var rowNum = 0;
            CheckerBoard currentBoard = null;

            for (int i = 0; i < _lineData.Length; i++)
            {
                if (i == 0)
                {
                    instructions = _lineData[i];
                    continue;
                }

                if (string.IsNullOrWhiteSpace(_lineData[i]))
                {
                    currentBoard = new CheckerBoard(man);
                    rowNum = 0;
                    continue;
                }
                else
                {
                    currentBoard.FeedLine(_lineData[i]);
                    rowNum++;
                }
            }

            return (man, instructions);
        }

        class CheckerBoardManager
        {
            private Dictionary<string, List<CheckerBoard>> _boards = new Dictionary<string, List<CheckerBoard>>();
            private Dictionary<CheckerBoard, bool> _allBoards = new Dictionary<CheckerBoard, bool>();
            public int RemainingBoards => _allBoards.Values.Where(x => !x).Count();
            internal bool FeedInstruction(string instruction, out string[] unhitPoints)
            {
                string[] aHit = null;
                var didHit = false;
                if (_boards.TryGetValue(instruction, out var boardsWithInstruction))
                {
                    foreach (var board in boardsWithInstruction)
                    {
                        if (board.HitPoint(instruction, out var unhitPoint))
                        {
                            _allBoards[board] = true;

                            if (didHit)
                                continue;

                            aHit = unhitPoint;
                            didHit = true;
                        }
                    }
                }
                if (!didHit)
                    unhitPoints = default;
                else
                    unhitPoints = aHit;

                return didHit;
            }

            internal void RegisterItem(string item, CheckerBoard checkerBoard)
            {
                if (!_allBoards.ContainsKey(checkerBoard))
                    _allBoards.Add(checkerBoard,false);

                if (!_boards.TryGetValue(item, out var boards))
                {
                    boards = new List<CheckerBoard>();
                    _boards.Add(item, boards);
                    boards.Add(checkerBoard);
                }
                else
                    boards.Add(checkerBoard);

                checkerBoard.RegisterContainer(boards);
            }
        }

        class CheckerBoard
        {

            private Dictionary<string, int> _columns = new Dictionary<string, int>();
            private Dictionary<string, int> _rows = new Dictionary<string, int>();

            private Dictionary<int, int> _hitRows = new Dictionary<int, int>();
            private Dictionary<int, int> _hitColumns = new Dictionary<int, int>();

            private List<List<CheckerBoard>> _registeredIn = new List<List<CheckerBoard>>();
            private CheckerBoardManager _manager;

            public CheckerBoard(CheckerBoardManager manager)
            {
                _manager = manager;
            }

            public void FeedLine(string line)
            {
                var nextRow = _rows.Any() ? _rows.Max(x => x.Value) + 1 : 0;
                int iterator = 0;
                foreach (var item in line.Trim().Split(" "))
                {
                    if (string.IsNullOrWhiteSpace(item))
                        continue;

                    if (!_columns.TryAdd(item, iterator))
                        throw new ArgumentException("column already contains value");

                    if (!_rows.TryAdd(item, nextRow))
                        throw new ArgumentException("Row already contains value");

                    _manager.RegisterItem(item, this);

                    iterator++;
                }
            }

            public bool HitPoint(string point, out string[] unhitPoints)
            {
                var wasHitCol = DoHit(_columns, _hitColumns, point, out var unhitPointsCol);
                var wasHitRow = DoHit(_rows, _hitRows, point, out var unhitPointsRow);

                if (wasHitCol && !wasHitRow)
                    unhitPoints = unhitPointsCol;
                else if (!wasHitCol && wasHitRow)
                    unhitPoints = unhitPointsRow;
                else if (wasHitCol && wasHitRow)
                    unhitPoints = unhitPointsRow;
                else
                    unhitPoints = default;

                return wasHitCol || wasHitRow;

                bool DoHit(Dictionary<string, int> hitIn, Dictionary<int, int> hitCounter, string point, out string[] unhitElements)
                {
                    if (hitIn.TryGetValue(point, out var result))
                    {
                        hitIn.Remove(point);
                        if (hitCounter.ContainsKey(result))
                        {
                            hitCounter[result]++;
                            if (hitCounter[result] == 5)
                            {
                                unhitElements = hitIn.Keys.ToArray();
                                return true;
                            }
                        }
                        else if (!hitCounter.ContainsKey(result))
                            hitCounter.Add(result, 1);
                    }
                    unhitElements = default;
                    return false;
                }
            }


            internal void RegisterContainer(List<CheckerBoard> boards)
            {
                if (!_registeredIn.Contains(boards))
                    _registeredIn.Add(boards);
            }
        }
    }
}
