using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Core
{
    [Serializable]
    public class Point
    {
        public int x, y;
        public Point(int x, int y) { this.x = x; this.y = y; }
        public string getAsString() => $"[{this.x};{this.y}]";
    }
}
