using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Movement;
using System.Net;

namespace Chess.Models.Figures
{
    class Figure
    {
        public string name;
        public Point position;
        public string color;
        public ConsoleColor colorColor;
        protected List<Point> movePoints = new List<Point>();
        public MoveSet moveSet = new MoveSet();
        public Board board;
        public int MoveCount = 0;

        public List<Point> ConvertRelativePointToAbsolute(List<Point> points)
        {
            List<Point> ress = new List<Point>();
            foreach (Point point in points)
            {
                Point resPoint = new Point(position.x + point.x, position.y + point.y);
                ress.Add(resPoint);
            }
            return ress;
        }

        public virtual List<Point> getMovePoints()
        {
            List<Point> res = new List<Point>();

            foreach (Move move in moveSet.moves)
            {
                List<Point> points = ConvertRelativePointToAbsolute(move.getMovePoints());

                if (move.requireEnemyChecking)
                {
                    foreach (Point point in points)
                    {
                        //Console.WriteLine(point.getAsString());

                        if (!board.isSlotEmpty(point))
                        {
                            Figure figure = board.GetFigureAtPoint(point);
                            if (board.FiguresIsEnemies(figure, this))
                            {
                                res.Add(point);
                            }
                            break;

                        }
                        else
                        {
                            res.Add(point);
                        }
                    }
                }
                else
                {
                    res.AddRange(ConvertRelativePointToAbsolute(move.getMovePoints()));
                    List<Point> clearPoints = new List<Point>();
                    foreach (Point point in res)
                    {
                        if (!board.isSlotEmpty(point))
                        {
                            Figure figure = board.GetFigureAtPoint(point);
                            if (!board.FiguresIsEnemies(figure, this))
                            {
                                clearPoints.Add(point);
                            }
                        }
                    }
                    res.RemoveAll(point => clearPoints.Any(clearPoints => point.x == clearPoints.x && point.y == clearPoints.y));
                }
            }
            return res;
        }
        public virtual void onFigureMoved()
        {
            this.MoveCount++;
        }
        public bool pointMovable(Point point)
        {
            List<Point> AvaliblePoints = new List<Point>();
            AvaliblePoints = getMovePoints();
            return AvaliblePoints.Any(pnt => pnt.x == point.x && pnt.y == point.y);
        }
        public void Move(Point newPosition, Board board)
        {
            if (!board.isSlotEmpty(newPosition))
            {
                board.removeFigureAt(newPosition);
            }
            else { position = newPosition; }
        }
        public void setPosition(Point newPoint) => position = newPoint;
        public Figure(Point point, string color, Board board)
        {
            this.position = point;
            this.color = color;
            this.name = "Base figure";
            this.board = board;

            switch (color)
            {
                case "Purple":
                    this.colorColor = ConsoleColor.Magenta;
                    break;
                case "Blue":
                    this.colorColor = ConsoleColor.Blue;
                    break;
                default:
                    this.colorColor = ConsoleColor.White;
                    break;
            }
            //this.movePoints.Add(new Point(0, 0));
        }
        public virtual bool IsPatternMoveValid(Point newPoint)
        {
            Point compareRelativePoint = new Point(newPoint.x - this.position.x, newPoint.y - this.position.y);
            //Console.WriteLine(compareRelativePoint.getAsString());

            //foreach (Point patt in movePoints)
            //{
            //    Console.WriteLine(patt.getAsString());
            //}

            return this.movePoints.IndexOf(compareRelativePoint) > 0 ? true : false;

            //return this.movePoints.Contains(compareRelativePoint);
        }
        public void setAvalibleMovePoints()
        {
            foreach (Move move in moveSet.moves)
            {
                this.movePoints.AddRange(move.MovePoints);
            }
        }
    }
}
