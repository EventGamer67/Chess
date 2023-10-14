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
    public class King : Figure
    {
        public King(Point point, string color, Board board) : base(point, color, board)
        {
            this.color = color;
            this.name = "King";
            this.position = point;
            this.board = board;

            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(1, 1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(1, 0)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(1, -1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(0, 1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(0, -1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-1, -1)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-1, 0)));
            this.moveSet.moves.Add(new SingleMoveWithoutCheckingPathWay(new Point(-1, 1)));

            this.setAvalibleMovePoints();
        }
        public List<Point> getSelfMovePattern()
        {
            List<Point> res = new List<Point>();

            foreach (Move move in moveSet.moves)
            {
                List<Point> points = ConvertRelativePointToAbsolute(move.getMovePoints());

                if (move.requireEnemyChecking)
                {
                    foreach (Point point in points)
                    {
                        res.Add(point);
                        if (!board.isSlotEmpty(point))
                        {
                            break;
                        }
                    }
                }
                else
                {
                    res.AddRange(ConvertRelativePointToAbsolute(move.getMovePoints()));
                }
            }
            return res;
        }
        public (bool leftRook,bool rightRook) RookSwapAvalible()
        {
            bool resRight = true;
            bool resLeft = true;
            Point leftRookPoint = new Point(1, this.position.y);
            Point rightRookPoint = new Point(board.width, this.position.y);

            if (!board.isSlotEmpty(rightRookPoint))
            {
                Figure rookRight = board.GetFigureAtPoint(rightRookPoint);
                if(rookRight is Rook)
                {
                    if (rookRight.MoveCount == 0)
                    {
                        for (int i = this.position.x + 1; i < rookRight.position.x - 1; i++)
                        {
                            if (!board.isSlotEmpty(new Point(i, this.position.y)))
                            {
                                resRight = false;
                            }
                        }
                    }
                }
                else
                {
                    resRight = false;
                }
            }
            else
            {
                resRight = false;
            }
            if (!board.isSlotEmpty(leftRookPoint))
            {
                Figure rookLeft = board.GetFigureAtPoint(leftRookPoint);
                if(rookLeft is Rook)
                {
                    if (rookLeft.MoveCount == 0)
                    {
                        for (int i = this.position.x - 1; i > rookLeft.position.x + 1; i--)
                        {
                            if (!board.isSlotEmpty(new Point(i, this.position.y)))
                            {
                                resLeft = false;
                            }
                        }
                    }
                }
                else
                {
                    resLeft = false;
                }
            }
            else
            {
                resLeft = false;
            }
            return (resLeft,resRight);
        }
        public override List<Point> getMovePoints()
        {
            List<Point> res = new List<Point>();

            foreach (Move move in moveSet.moves)
            {
                List<Point> points = ConvertRelativePointToAbsolute(move.getMovePoints());

                if (move.requireEnemyChecking)
                {
                    foreach (Point point in points)
                    {
                        res.Add(point);
                        if (!board.isSlotEmpty(point))
                        {
                            break;
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
            //rokirovka
            if (this.RookSwapAvalible().leftRook == true)
            {
                res.Add(new Point(1, this.position.y));
                Console.WriteLine("leftrook");
            }
            if (this.RookSwapAvalible().rightRook == true)
            {
                res.Add(new Point(board.width, this.position.y));
                Console.WriteLine("rightrook");
            }
            //cleaning points
            List<String> colors = new List<String>();
            colors = board.game.Players.ToList();
            colors.Remove(this.color);
            foreach (String color in colors)
            {
                List<Point> teamPoints = board.getTeamMovePoints(color,true);
                teamPoints = teamPoints.Distinct().ToList();
                res.RemoveAll(point => teamPoints.Any(pointTeam => point.x == pointTeam.x && point.y == pointTeam.y));
            }
            res = board.clearNonValid(res);
            return res;
        }
    }

}
