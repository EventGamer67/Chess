using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Movement
{
    [Serializable]

    class SingleMoveWithoutCheckingPathWay : Move
    {
        public SingleMoveWithoutCheckingPathWay(Point point)
        {
            this.requireEnemyChecking = false;
            this.MovePoints.Add(point);
        }
        protected override List<Point> getPoints()
        {
            return MovePoints;
        }
    }

}
