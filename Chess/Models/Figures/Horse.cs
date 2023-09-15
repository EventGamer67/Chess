using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Movement;

namespace Chess.Models.Figures
{
    class Horse : Figure
    {
        public Horse(Point point, string color, Board board) : base(point, color, board)
        {
            this.color = color;
            this.name = "Horse";
            this.position = point;
            this.board = board;

            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(1, 2)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-1, 2)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(1, -2)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-1, -2)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(2, 1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(2, -1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-2, 1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-2, -1)));

            this.setAvalibleMovePoints();
        }
    }

}
