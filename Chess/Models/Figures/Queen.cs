using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Movement;

namespace Chess.Models.Figures
{
    [Serializable]
    public class Queen : Figure
    {
        public Queen(Point point, string color, Board board) : base(point, color, board)
        {
            this.color = color;
            this.name = "Queen";
            this.position = point;
            this.board = board;

            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(1, 1)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(-1, -1)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(-1, 1)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(1, -1)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(1, 0)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(-1, 0)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(0, 1)));
            this.moveSet.moves.Add(new LineMoveCheckingPathWay(new Point(0, -1)));
        }
    }

}
