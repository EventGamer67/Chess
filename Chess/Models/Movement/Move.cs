using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Movement
{
    [Serializable]

    public class Move
    {
        public List<Point> getMovePoints()
        {
            return getPoints();
        }
        public List<Point> MovePoints = new List<Point>();

        public bool requireEnemyChecking = false;
        public bool canMoveAlongPath(Board board, List<Point> path)
        {
            foreach (Point point in path)
            {
                if (!board.isSlotEmpty(point))
                {
                    return false;
                }
            }
            return true;
        }
        protected virtual List<Point> getPoints()
        {
            return new List<Point>();
        }
        protected virtual List<Point> getPoints(Point startPoint)
        {
            return new List<Point>();
        }
    }

}
