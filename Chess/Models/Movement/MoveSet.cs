using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Movement
{
    [Serializable]
    class MoveSet
    {
        public List<Move> moves = new List<Move>();
        public Move getMove(Point destinaton, int offsetX, int offsetY)
        {
            foreach (Move move in moves)
            {
                if (move.MovePoints.Any(point => point.x == (destinaton.x - offsetX) && point.y == (destinaton.y - offsetY)))
                {
                    return move;
                }
            }
            return null;
        }
    }

}
