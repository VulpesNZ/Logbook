using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core
{
    public static class Extensions
    {
        public static string HyphenIfZero(this int i)
        {
            return (i == 0) ? "-" : i.ToString();
        }
    }
}
