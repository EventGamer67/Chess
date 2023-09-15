using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Tools;

namespace Chess.Models.Core
{
    class Game
    {
        bool running = false;
        Board board;
        public List<String> Players = new List<String>();
        int currentPlayerIndex = 0;
        public void startGame()
        {
            board.resetBoard();
            running = true;

            Players.Add("Purple");
            Players.Add("Blue");

            board.setFigure(new Horse(new Point(0, 0), "Purple", board), new Point(2, 1));
            board.setFigure(new Horse(new Point(0, 0), "Purple", board), new Point(7, 1));

            board.setFigure(new Rook(new Point(0, 0), "Purple", board), new Point(1, 1));
            board.setFigure(new Rook(new Point(0, 0), "Purple", board), new Point(8, 1));

            board.setFigure(new Bishop(new Point(0, 0), "Purple", board), new Point(6, 1));
            board.setFigure(new Bishop(new Point(0, 0), "Purple", board), new Point(3, 1));

            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(1, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(2, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(3, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(4, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(5, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(6, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(7, 2));
            board.setFigure(new Pawn(new Point(0, 0), "Purple", board, new Point(0, 1)), new Point(8, 2));

            board.setFigure(new Queen(new Point(0, 0), "Purple", board), new Point(5, 1));
            board.setFigure(new King(new Point(0, 0), "Purple", board), new Point(4, 1));


            board.setFigure(new Horse(new Point(0, 0), "Blue", board), new Point(2, 8));
            board.setFigure(new Horse(new Point(0, 0), "Blue", board), new Point(7, 8));

            board.setFigure(new Rook(new Point(0, 0), "Blue", board), new Point(1, 8));
            board.setFigure(new Rook(new Point(0, 0), "Blue", board), new Point(8, 8));

            board.setFigure(new Bishop(new Point(0, 0), "Blue", board), new Point(6, 8));
            board.setFigure(new Bishop(new Point(0, 0), "Blue", board), new Point(3, 8));

            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(1, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(2, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(3, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(4, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(5, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(6, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(7, 7));
            board.setFigure(new Pawn(new Point(0, 0), "Blue", board, new Point(0, -1)), new Point(8, 7));

            board.setFigure(new Queen(new Point(0, 0), "Blue", board), new Point(4, 8));
            board.setFigure(new King(new Point(0, 0), "Blue", board), new Point(5, 8));


            while (running)
            {
                board.DisplayBoard();

                //Console.WriteLine(board.GetFiguresAsString(null));

                Console.WriteLine($"\n {Players[currentPlayerIndex]} select figure coordinate");

                Console.WriteLine("x:");
                string selectedX = Console.ReadLine();
                if (!Tools.Tools.pointAxisValid(selectedX))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid axis point");
                    continue;
                }
                Console.WriteLine("y:");
                string selectedY = Console.ReadLine();
                if (!Tools.Tools.pointAxisValid(selectedY))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid axis point");
                    continue;
                }

                Point selectedPoint = new Point(int.Parse(selectedX), int.Parse(selectedY));

                Figure selectedFigure = board.GetFigureAtPoint(selectedPoint);

                if (selectedFigure == null)
                {
                    Console.Clear();
                    Console.WriteLine("No figure found at the selected position. Try again.");
                    continue;
                }

                if (selectedFigure.color != Players[currentPlayerIndex])
                {
                    Console.Clear();
                    Console.WriteLine("not self figure");
                    continue;
                }

                Console.Clear();
                Console.WriteLine($"Selected figure: {selectedFigure.name} {selectedFigure.color} {selectedFigure.position.getAsString()}");
                board.DisplayFigureMoves(selectedFigure);

                Console.WriteLine("move to x:");
                selectedX = Console.ReadLine();
                if (!Tools.Tools.pointAxisValid(selectedX))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid axis point");
                    continue;
                }
                Console.WriteLine("move to y:");
                selectedY = Console.ReadLine();
                if (!Tools.Tools.pointAxisValid(selectedY))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid axis point");
                    continue;
                }
                Point destinationPoint = new Point(int.Parse(selectedX), int.Parse(selectedY));

                if (!board.PointValid(destinationPoint))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid board point");
                    continue;
                }

                string respone = board.FigureCanMoveToPoint(selectedFigure, destinationPoint);
                if (respone == "Ok")
                {
                    board.MoveFigure(selectedFigure, destinationPoint);
                    Console.Clear();
                    currentPlayerIndex++;
                    if (currentPlayerIndex == Players.Count)
                    {
                        currentPlayerIndex = 0;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Cannot move here {respone}");
                }
            }
        }

        public Game()
        {
            this.board = new Board(8, 8, this);
        }
    }

}
