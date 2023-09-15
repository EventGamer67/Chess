using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Movement
{
    class LineMoveCheckingPathWay : Move
    {
        Point deltaWay;
        int Border = 15;
        public LineMoveCheckingPathWay(Point deltaPoint)
        {
            this.requireEnemyChecking = true;
            this.deltaWay = deltaPoint;
        }
        protected override List<Point> getPoints()
        {
            MovePoints.Clear();
            for (int i = 1; i < Border; i++)
            {
                this.MovePoints.Add(new Point(deltaWay.x * i, deltaWay.y * i));
            }
            return MovePoints;
        }
    }

}
