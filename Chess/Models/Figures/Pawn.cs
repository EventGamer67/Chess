using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Movement;
using System.Collections;

namespace Chess.Models.Figures
{
    class Pawn : Figure
    {
        protected bool doubleMoveAvalible = true;
        public Pawn(Point point, string color, Board board, Point dir) : base(point, color, board)
        {
            this.color = color;
            this.name = "Pawn";
            this.position = point;
            this.board = board;

            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(dir));

            this.setAvalibleMovePoints();
        }
        //тестово сделать без проверок на предыдущую позицию
        public override void onFigureMoved()
        {
            base.onFigureMoved();
            doubleMoveAvalible = false;

            Move move = moveSet.moves[0];
            List<Point> res = ConvertRelativePointToAbsolute(move.getMovePoints());
            Point movePoint = move.MovePoints[0];

            Point CheckEnemyFigure = new Point(this.position.x + movePoint.x, this.position.y - movePoint.y);
            if (!board.isSlotEmpty(CheckEnemyFigure))
            {
                Figure figure = board.GetFigureAtPoint(CheckEnemyFigure);
                if (board.FiguresIsEnemies(this, figure))
                {
                    board.removeFigure(figure);
                }
            }

        }
        public override List<Point> getMovePoints()
        {
            Move move = moveSet.moves[0];
            List<Point> res = ConvertRelativePointToAbsolute(move.getMovePoints());
            Point movePoint = move.MovePoints[0];
            if (!board.isSlotEmpty(new Point(position.x + movePoint.x, position.y + movePoint.y)))
            {
                res.Clear();
            }
            else
            {
                if (doubleMoveAvalible)
                {
                    Point point = new Point(position.x + movePoint.x + movePoint.x, position.y + movePoint.y + movePoint.y);
                    //Console.WriteLine("special move:" + point.getAsString());
                    res.Add(point);
                }
            }
            //WARNING MOMENT IN OFFSET
            Point comparer = new Point(0,0);
            Point comparer2 = new Point(0, 0);


            if (movePoint.y == 1)
            {
                comparer = new Point(position.x + movePoint.x - 1, position.y + movePoint.y);
                comparer2 = new Point(position.x + movePoint.x + 1, position.y + movePoint.y);
                
            }
            if (movePoint.y == -1)
            {
                comparer = new Point(position.x + movePoint.x - 1, position.y + movePoint.y);
                comparer2 = new Point(position.x + movePoint.x + 1, position.y + movePoint.y);
            }
            if (!board.isSlotEmpty(comparer))
            {
                if (board.FiguresIsEnemies(this, board.GetFigureAtPoint(comparer)))
                {
                    res.Add(comparer);
                }
            }
            if (!board.isSlotEmpty(comparer2))
            {
                if (board.FiguresIsEnemies(this, board.GetFigureAtPoint(comparer2)))
                {
                    res.Add(comparer2);
                }
            }

            //vzatie na proxode

            Point tempCheckerLeft;
            Point tempCheckerRight;

            tempCheckerLeft = new Point(position.x - 1, position.y);
            tempCheckerRight = new Point(position.x + 1, position.y);

            if (!board.isSlotEmpty(tempCheckerLeft))
            {
                if( (board.FiguresIsEnemies(this,board.GetFigureAtPoint(tempCheckerLeft)) == true))
                {
                    if (movePoint.y == 1)
                    {
                        if(board.isSlotEmpty(new Point(position.x - 1, position.y+ 1)))
                        {
                            res.Add(new Point(position.x - 1, position.y + 1));
                        }
                    }
                    if (movePoint.y == -1)
                    {
                        if (board.isSlotEmpty(new Point(position.x - 1, position.y - 1)))
                        {
                            res.Add(new Point(position.x - 1, position.y - 1));
                        }
                    }
                }
            }

            if (!board.isSlotEmpty(tempCheckerRight))
            {
                if ((board.FiguresIsEnemies(this, board.GetFigureAtPoint(tempCheckerRight)) == true))
                {
                    if (movePoint.y == 1)
                    {
                        if (board.isSlotEmpty(new Point(position.x + 1, position.y + 1)))
                        {
                            res.Add(new Point(position.x + 1, position.y + 1));
                        }
                    }
                    if (movePoint.y == -1)
                    {
                        if (board.isSlotEmpty(new Point(position.x + 1, position.y - 1)))
                        {
                            res.Add(new Point(position.x + 1, position.y - 1));
                        }
                    }
                }
            }
            //foreach (Point point in res)
            //{
            //    Console.WriteLine(point.getAsString());
            //}
            return res;
        }
    }

}
