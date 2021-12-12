using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Library.Helpers
{
    public class ResultParsers
    {
        public string IntToString(int res) => res.ToString();
        public string Def(object obj) => obj.ToString();
    }
}
