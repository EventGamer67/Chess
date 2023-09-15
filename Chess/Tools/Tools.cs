using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Tools
{
    public static class Tools
    {
        public static bool pointAxisValid(string pointAxis)
        {
            if (string.IsNullOrWhiteSpace(pointAxis))
            {
                return false;
            }

            if (!int.TryParse(pointAxis, out _))
            {
                return false;
            }
            return true;
        }
    }
}
