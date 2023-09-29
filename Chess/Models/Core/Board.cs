using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Figures;
using Chess.Models.Movement;

namespace Chess.Models.Core
{
    [Serializable]
    class Board
    {
        public int width, height;
        protected List<Figure> figures;
        public Game game;
        public Board(int width, int height, Game game)
        {
            this.width = width;
            this.height = height;
            this.game = game;
            figures = new List<Figure>();
        }

        public void DisplayFigureMoves(Figure figure)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            List<Point> movePoints = figure.getMovePoints();

            //foreach(Point point in movePoints)
            //{
            //    Console.WriteLine(point.getAsString());

            //}

            Console.Write("  ");
            for (int x = 1; x <= width; x++)
            {
                Console.Write($" {x} ");
            }
            Console.WriteLine();
                
            for (int y = height; y >= 1; y--)
            {
                Console.Write($"{y} ");

                for (int x = 1; x <= width; x++)
                {
                    Figure figureAtPoint = GetFigureAtPoint(new Point(x, y));

                    char symbolToDisplay = ' ';
                    Console.ForegroundColor = ConsoleColor.White;
                    if (figureAtPoint != null)
                    {
                        symbolToDisplay = GetSymbolForFigure(figureAtPoint);
                        Console.ForegroundColor = figureAtPoint.colorColor;
                    }
                    if (movePoints.Any(point => point.x == x && point.y == y))
                    {
                        if (symbolToDisplay == ' ')
                        {
                            symbolToDisplay = '.';
                        }
                        if (figureAtPoint != null)
                        {
                            if (FiguresIsEnemies(figure, figureAtPoint))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;

                            }
                            else
                            {
                                //Console.ForegroundColor = figureAtPoint.colorColor;
                                
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        {
                        }
                        Console.Write($"[{symbolToDisplay}]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write($"[{symbolToDisplay}]");
                    }
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public Board DeepCopy()
        {
            return DeepCopyHelper.DeepCopy(this);
        }
        public bool IsMyKingAttacked(Board board, string myColor)
        {
            Figure myKing = board.getTeamFigures(myColor).Where(figure => figure is King).ToList()[0];

            List<string> colors = new List<string>();
            colors = board.game.Players.ToList();
            colors.Remove(myColor);

            foreach (string color in colors)
            {
                List<Point> enemyPoints = board.getTeamMovePoints(color,false);
                enemyPoints = enemyPoints.Distinct().ToList();
                foreach (Point enemyPoint in enemyPoints)
                {
                    if (myKing.position.x == enemyPoint.x && myKing.position.y == enemyPoint.y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsMyKingSaveable(Board board, string myColor)
        {
            //List<Point> moves = board.getTeamMovePoints(myColor, false);
            List<Point> moves = board.getTeamMovePoints(myColor,true); 
            return (moves.Count > 0 ? true : false);
        }
        public void DisplayBoard()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  ");
            for (int x = 1; x <= width; x++)
            {
                Console.Write($" {x} ");
            }
            Console.WriteLine();

            for (int y = height; y >= 1; y--)
            {
                Console.Write($"{y} ");

                for (int x = 1; x <= width; x++)
                {
                    Figure figureAtPoint = GetFigureAtPoint(new Point(x, y));

                    char symbolToDisplay = ' ';

                    if (figureAtPoint != null)
                    {
                        symbolToDisplay = GetSymbolForFigure(figureAtPoint);
                        Console.ForegroundColor = figureAtPoint.colorColor;

                        if (figureAtPoint is King)
                        {
                            if (IsMyKingAttacked(this, figureAtPoint.color))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                    }
                    Console.Write($"[{symbolToDisplay}]");

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }
        }
        public bool FiguresIsEnemies(Figure figure1, Figure figure2)
        {
            if (figure1.color == figure2.color)
            {
                return false;
            }
            return true;
        }
        private char GetSymbolForFigure(Figure figure)
        {
            if (figure != null)
            {
                char symbolToDisplay;
                switch (figure.name)
                {
                    case "Base figure":
                        symbolToDisplay = 'A';
                        break;
                    case "Horse":
                        symbolToDisplay = 'H';
                        break;
                    case "Rook":
                        symbolToDisplay = 'R';
                        break;
                    case "Bishop":
                        symbolToDisplay = 'B';
                        break;
                    case "Queen":
                        symbolToDisplay = 'Q';
                        break;
                    case "Pawn":
                        symbolToDisplay = 'P';
                        break;
                    case "King":
                        symbolToDisplay = 'K';
                        break;
                    default:
                        symbolToDisplay = ' ';
                        break;
                }
                return symbolToDisplay;
            }
            else { return ' '; }
        }
        public bool isSlotEmpty(Point point) => !figures.Any(figure => figure.position.x == point.x && figure.position.y == point.y);
        public string FigureCanMoveToPoint(Figure figure, Point point)
        {
            if (figure.pointMovable(point))
            {
                return "Ok";
            }
            else
            {
                return "It point is not figure move point";
            }
        }
        public bool PointValid(Point point)
        {
            return point.x >= 1 && point.x <= width && point.y >= 1 && point.y <= height;
        }
        public void resetBoard() => figures.Clear();
        public void setFigure(Figure figure, Point point)
        {
            Figure setFigure = figure;
            setFigure.position = point;
            figures.Add(setFigure);
        }
        public List<Figure> getTeamFigures(string color)
        {
            List<Figure> teams = new List<Figure>();
            teams = this.figures.Where(figure => figure.color == color).ToList();
            return teams;
        }
        public List<Point> getFiguresPositionPoint(List<Figure> figures)
        {
            List<Point> points = new List<Point>();
            foreach(Figure figure in figures)
            {
                points.Add(figure.position);
            }
            return points;
        }
        public Figure GetFigureAtPoint(Point point) => this.figures.FirstOrDefault(figure => figure.position.x == point.x && figure.position.y == point.y);
        public string GetFiguresAsString(string filter)
        {
            var filteredFigures = filter != null ? figures.Where(figure => figure.color == filter) : figures;
            var stringBuilder = new StringBuilder();
            foreach (var figure in filteredFigures.OrderByDescending(figure => figure.color))
            {
                stringBuilder.AppendLine($"<{figure.color}> {figure.name} {figure.position.getAsString()}");
            }
            return stringBuilder.ToString();
        }
        public void removeFigure(Figure figure) => this.figures.Remove(figure);
        public void removeFigureAt(Point point) => this.figures.Remove(GetFigureAtPoint(point));
        public void MoveFigure(Figure figure, Point newPosition)
        {
            if (!isSlotEmpty(newPosition))
            {
                Figure figureOnPositon = this.GetFigureAtPoint((Point)newPosition);
                if (!this.FiguresIsEnemies(figureOnPositon, figure))
                {
                    if (figureOnPositon is Rook)
                    {
                        //right swap
                        if (figureOnPositon.position.x > figure.position.x)
                        {
                            newPosition = new Point(figure.position.x + 2, figure.position.y);
                            this.MoveFigure(figureOnPositon, new Point(figure.position.x + 1,figure.position.y));
                        }
                        //left swap
                        else
                        {
                            newPosition = new Point(figure.position.x - 2, figure.position.y);
                            this.MoveFigure(figureOnPositon, new Point(figure.position.x - 1, figure.position.y));
                        }
                    }
                }
                else
                {
                    removeFigureAt(newPosition);
                }
            }
            figure.setPosition(newPosition);
            figure.onFigureMoved();
        }
        public List<Point> clearNonValid(List<Point> points)
        {
            List<Point> filtered = new List<Point>();
            foreach (Point point in points)
            {
                if (this.PointValid(point))
                {
                    filtered.Add(point);
                }
            }
            return filtered;
        }
        public List<Point> getTeamMovePoints(string color,bool recursion)
        {
            List<Figure> teamFigures = figures.Where(figure => figure.color == color).ToList();
            List<Point> movePoints = new List<Point>();
            foreach (Figure figure in teamFigures)
            {
                if (!(figure is King))
                {
                    if (recursion)
                    {
                        movePoints.AddRange(figure.getMovePoints());
                    }
                    else
                    {
                        movePoints.AddRange(figure.getMovePointsWithoutFiltering());
                    }
                }
                else
                {
                    movePoints.AddRange(((King)figure).getSelfMovePattern());
                }
            }
            return movePoints;
        }
    }
}
