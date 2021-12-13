using AoC.Core.Dep;
using AoC.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AoC.Core.Engine
{
    public class AocEngine
    {
        private readonly Func<string> _getValueFromUser;
        private readonly Action<string> _sendMessageToUser;
        private Dictionary<int, Dictionary<int, ISolution>> _solutions;

        public AocEngine(Func<string> getValueFromUser, Action<string> sendMessageToUser)
        {
            var aocSolution = Assembly.Load("AoC.Solutions");
            var inheritedTypes = aocSolution.GetTypes().Where(type => typeof(ISolution).IsAssignableFrom(type));

            _solutions = inheritedTypes.Select(type => Injector.GetObject<ISolution>(type)).GroupBy(k => k.Year).Aggregate(new Dictionary<int, Dictionary<int, ISolution>>(), (dict, group) =>
            {
                if(!dict.TryGetValue(group.Key, out var activeYear)){

                    activeYear = new Dictionary<int, ISolution>();

                    dict.Add(group.Key, activeYear);
                }

                dict[group.Key] = group.ToDictionary(k => k.Day, v => v);

                return dict;
            });

            _getValueFromUser = getValueFromUser;
            _sendMessageToUser = sendMessageToUser;
        }

        public async Task Start()
        {
            _sendMessageToUser("Welcome to AoC, please choose a year: " + string.Join(", ", _solutions.Select(x => x.Key)));
            _sendMessageToUser("Otherwise, press return to get latest solution");
            var valueFromUser = _getValueFromUser();

            if (!string.IsNullOrWhiteSpace(valueFromUser) && 
                int.TryParse(valueFromUser, out int year) && 
                _solutions.TryGetValue(year, out var yearSolutions))
            {
                _sendMessageToUser("Todo:Implement");//TODO
            } else
            {
                _sendMessageToUser("Solving latest");
                var maxYear = _solutions.Max(x => x.Key);
                var maxYearDict = _solutions[maxYear];
                var maxDay = maxYearDict.Max(x => x.Key);
                var maxDaySolution = maxYearDict[maxDay];

                await maxDaySolution.Init();

                _sendMessageToUser(maxDaySolution.SolvePartOne());
                _sendMessageToUser(maxDaySolution.SolvePartTwo());
            }

        }
    }
}
