using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Figures;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace Chess.Models.Core.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void DisplayBoard_Test()
        {
            Game game= new Game();
            Board board = new Board(8, 8, game, false);
            try
            {
                board.DisplayBoard();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void DisplayBoardWithFigures_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figure = new Figure(new Point(2, 2), "Blue", board);
            board.setFigure(figure,new Point(2,2));
            try
            {
                board.DisplayBoard();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void DisplayBoardWithFiguresWithOverlappedKing_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen = new King(new Point(2, 2), "Purple", board);
            var figureQueen2 = new Queen(new Point(6, 5), "Purple", board);
            var figureKing = new Queen(new Point(5, 5), "Blue", board);
            board.setFigure(figureQueen, new Point(2, 2));
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureQueen2, new Point(6, 5));
            try
            {
                board.DisplayBoard();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void FiguresIsEnemies_returnTrue_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen = new King(new Point(2, 2), "Purple", board);
            var figureQueen2 = new Queen(new Point(6, 5), "Blue", board);
            Assert.IsTrue(board.FiguresIsEnemies(figureQueen, figureQueen2));
        }
        [TestMethod()]
        public void FiguresIsEnemies_returnFalse_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen = new King(new Point(2, 2), "Purple", board);
            var figureQueen2 = new Queen(new Point(6, 5), "Purple", board);
            Assert.IsFalse(board.FiguresIsEnemies(figureQueen, figureQueen2));
        }
        [TestMethod()]
        public void PointValid_returnTrue_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            Assert.IsTrue(board.PointValid(new Point(3, 3)));  
        }
        [TestMethod()]
        public void PointValid_returnFalse_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            Assert.IsFalse(board.PointValid(new Point(-1, -1)));
        }
        [TestMethod()]
        public void GetFiguresAsString_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen = new King(new Point(2, 2), "Purple", board);
            var figureQueen2 = new Queen(new Point(6, 5), "Purple", board);
            var figureQueen3 = new Queen(new Point(6, 5), "Blue", board);
            board.GetFiguresAsString("Purple");
        }
        [TestMethod()]
        public void GetFiguresAsString_filtered_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen = new King(new Point(2, 2), "Purple", board);
            var figureQueen2 = new Queen(new Point(6, 5), "Purple", board);
            var figureQueen3 = new Queen(new Point(6, 5), "Blue", board);
            board.GetFiguresAsString("Blue");
        }
        [TestMethod()]
        public void GetFiguresAsString_null_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen = new King(new Point(2, 2), "Purple", board);
            var figureQueen2 = new Queen(new Point(6, 5), "Purple", board);
            var figureQueen3 = new Queen(new Point(6, 5), "Blue", board);
            board.GetFiguresAsString(null);
        }
        [TestMethod()]
        public void DeepCopy_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            try
            {
                Board board1 = board.DeepCopy();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void FigureCanMoveToPoint_returnTrue_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureQueen2 = new Queen(new Point(5, 5), "Purple", board);
            board.setFigure(figureQueen2, new Point(5, 5));
            Assert.IsTrue(board.FigureCanMoveToPoint(figureQueen2, new Point(6, 6)) == "Ok");
        }
        [TestMethod()]
        public void FigureCanMoveToPoint_returnFalse_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figurePawn = new Pawn(new Point(5, 5), "Purple", board, new Point(0,1));
            board.setFigure(figurePawn, new Point(5, 5));
            Assert.IsFalse(board.FigureCanMoveToPoint(figurePawn, new Point(7, 7)) == "Ok");
        }
        [TestMethod()]
        public void resetBoard_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figurePawn = new Pawn(new Point(5, 5), "Purple", board, new Point(0, 1));
            board.setFigure(figurePawn, new Point(5, 5));
            board.resetBoard();
        }
        [TestMethod()]
        public void removeFigure_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figurePawn = new Pawn(new Point(5, 5), "Purple", board, new Point(0, 1));
            board.removeFigure(figurePawn);
        }
        [TestMethod()]
        public void removeFigureAt_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureRook = new Rook(new Point(5, 5), "Purple", board);
            board.removeFigureAt(new Point(5, 5));
        }
        [TestMethod()]
        public void getFiguresPositionPoint_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureRook = new Rook(new Point(5, 5), "Purple", board);
            board.getFiguresPositionPoint(new List<Figure> { figureRook });
        }
        [TestMethod()]
        public void IsMyKingSaveable_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureRook = new King(new Point(5, 5), "Purple", board);
            board.IsMyKingSaveable(board, "Purple");
        }
        [TestMethod()]
        public void IsMyKingSaveable_returnFalse_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(3, 3), "Purple", board);
            board.setFigure(figureKing, new Point(3,3));
            var figureQueen = new Queen(new Point(4, 4), "Blue", board);
            var figureQueen2 = new Queen(new Point(2, 2), "Blue", board);
            var figureQueen3 = new Queen(new Point(2, 2), "Blue", board);
            var figureQueen4 = new Queen(new Point(2, 2), "Blue", board);
            board.setFigure(figureQueen, new Point(3,5));
            board.setFigure(figureQueen2, new Point(3,1));
            board.setFigure(figureQueen3, new Point(1,3));
            board.setFigure(figureQueen4, new Point(5,3));
            Assert.IsTrue(board.IsMyKingSaveable(board, "Purple"));
        }

        [TestMethod()]
        public void MoveFigure_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(6, 6));
            board.MoveFigure(figureKing, new Point(6, 6));
        }
        [TestMethod()]
        public void MoveFigure_swap_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));
            board.MoveFigure(figureKing, new Point(4, 4));
        }
        [TestMethod()]
        public void MoveFigure_Kill_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));
            board.MoveFigure(figureKing, new Point(4, 4));
        }
        [TestMethod()]
        public void getTeamMovePoints_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));

            board.getTeamMovePoints("Purple",false);
        }
        [TestMethod()]
        public void getTeamMovePoints_recursion_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));

            board.getTeamMovePoints("Purple", true);
        }
        [TestMethod()]
        public void getTeamMovePoints_nonKing_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));

            board.getTeamMovePoints("Blue", true);
        }
        [TestMethod()]
        public void getTeamMovePoints_nonKing_recursion_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));

            board.getTeamMovePoints("Blue", false);
        }
        [TestMethod()]
        public void DisplayFigureMoves_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureKing, new Point(5, 5));
            board.setFigure(figureRook, new Point(4, 4));

            board.DisplayFigureMoves(figureKing);
        }
        [TestMethod()]
        public void DisplayFigureMoves_green_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Purple", board);
            board.setFigure(figureRook, new Point(8, 5));

            board.DisplayFigureMoves(figureKing);
        }
        [TestMethod()]
        public void IsMyKingAttacked_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            board.game.Players.Add("Purple");
            board.game.Players.Add("Blue");
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureRook = new Rook(new Point(5, 5), "Purple", board);
            board.setFigure(figureRook, new Point(8, 5));
            var figureRookBlue = new Rook(new Point(5, 5), "Blue", board);
            board.setFigure(figureRookBlue, new Point(3, 3));
            
            board.IsMyKingAttacked(board,"Purple");
        }
        public void IsMyKingAttacked_returnTrue_Test()
        {
            Game game = new Game();
            Board board = new Board(8, 8, game, false);
            board.game.Players.Add("Purple");
            board.game.Players.Add("Blue");
            var figureKing = new King(new Point(5, 5), "Purple", board);
            board.setFigure(figureKing, new Point(5, 5));
            var figureQueenBlue = new Queen(new Point(7, 7), "Blue", board);
            board.setFigure(figureQueenBlue, new Point(7, 7));

            Assert.IsTrue(board.IsMyKingAttacked(board, "Purple"));
        }
    }
}