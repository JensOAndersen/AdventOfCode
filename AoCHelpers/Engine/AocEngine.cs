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
        private Dictionary<int, Dictionary<int, ISolution>> _solutions;

        public AocEngine()
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
        }
    }
}
