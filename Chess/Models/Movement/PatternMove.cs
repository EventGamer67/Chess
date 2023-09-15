using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Movement
{
    class PatternMove : Move
    {
        public PatternMove(bool requireEnemyChecking, List<Point> points)
        {
            this.requireEnemyChecking = requireEnemyChecking;
            this.MovePoints = points;
        }
    }

}
