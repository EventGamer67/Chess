using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Core;
using System.Collections;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Chess.Models.Figures.Tests
{
    [TestClass()]
    public class FigureTests
    {
        [TestMethod]
        public void Figure_ConvertRelativePointToAbsolute_ReturnsCorrectPoints()
        {
            var game = new Game();
            var board = new Board(8,8,game,false);
            var figure = new Figure(new Point(2, 2), "Blue", board);
            var relativePoints = new List<Point> { new Point(1, 1), new Point(-1, -1) };
            var expectedAbsolutePoints = new List<Point> { new Point(3, 3), new Point(1, 1) };

            List<Point> absolutePoints = figure.ConvertRelativePointToAbsolute(relativePoints);

            Assert.AreEqual(expectedAbsolutePoints.Count, absolutePoints.Count);

            for (int i = 0; i < expectedAbsolutePoints.Count; i++)
            {
                Assert.AreEqual(expectedAbsolutePoints[i].x, absolutePoints[i].x);
                Assert.AreEqual(expectedAbsolutePoints[i].y, absolutePoints[i].y);
            }
        }
        [TestMethod()]
        public void figureCanMove_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(2, 2), "Blue", board, new Point(0,1));
            board.setFigure(figure, new Point(2, 2));
            Assert.IsTrue(figure.figureCanMove());
        }
        [TestMethod()]
        public void figureCanMove_returnFalse_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(1, 8), "Blue", board, new Point(0, 1));
            Assert.IsFalse(figure.figureCanMove());
        }

        [TestMethod()]
        public void getMovePoints_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Queen(new Point(2, 2), "Blue", board);
            var figureKing = new King(new Point(2, 2), "Purple", board);
            board.setFigure(figure, new Point(2, 2));
            board.setFigure(figureKing, new Point(2, 4));
            Assert.IsNotNull(figure.getMovePoints());
        }

        [TestMethod()]
        public void getMovePoints_2_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Horse(new Point(2, 2), "Purple", board);
            var figureKing = new King(new Point(2, 2), "Purple", board);
            board.setFigure(figure, new Point(2, 2));
            board.setFigure(figureKing, new Point(3, 4));
            Assert.IsNotNull(figure.getMovePoints());
        }

        [TestMethod()]
        public void onFigureMovedTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);

            var figure = new Figure(new Point(2, 2), "Blue", board);

            int moveCountBefore = figure.MoveCount;

            board.MoveFigure(figure, new Point(figure.position.x, figure.position.y + 1));

            Assert.IsTrue(moveCountBefore < figure.MoveCount);
        }

        [TestMethod()]
        public void setPositionTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Figure(new Point(2, 2), "Blue", board);

            figure.setPosition(new Point(1, 1));

            Assert.IsTrue(figure.position.x == 1 && figure.position.y == 1);
        }

        [TestMethod()]
        public void FigureTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Figure(new Point(2, 2), "Blue", board);
            var figure1 = new Figure(new Point(3, 3), "Purple", board);
            var figure2 = new Figure(new Point(4, 4), "none", board);
        }

        [TestMethod()]
        public void IsPatternMoveValid_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(2, 2), "Blue", board, new Point(1, 0));
            board.setFigure(figure, new Point(2, 2));
            Assert.IsFalse(figure.IsPatternMoveValid(new Point(0,2)));
        }

        [TestMethod()]
        public void IsPatternMoveValid_returnFalse_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(2, 2), "Blue", board, new Point(0, 1));
            board.setFigure(figure, new Point(2, 2));
            Assert.IsFalse(figure.IsPatternMoveValid(new Point(0, 3)));
        }

        [TestMethod()]
        public void setAvalibleMovePointsTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(2, 2), "Blue", board, new Point(1,0));

            figure.setAvalibleMovePoints();
            board.setFigure(figure, new Point(2, 2));
            Assert.IsNotNull(figure.getMovePoints());
        }

        [TestMethod()]
        public void getMovePointsWithoutFiltering_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(2, 2), "Blue", board, new Point(1, 0));
            var figureKing = new King(new Point(2, 2), "Purple", board);
            board.setFigure(figure, new Point(2, 2));
            board.setFigure(figureKing, new Point(2, 4));
            Assert.IsNotNull(figure.getMovePointsWithoutFiltering());
        }

        [TestMethod()]
        public void getMovePointsWithoutFiltering_2_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Queen(new Point(2, 2), "Blue", board);
            var figureKing = new King(new Point(2, 2), "Purple", board);
            board.setFigure(figure, new Point(2, 2));
            board.setFigure(figureKing, new Point(2, 4));
            Assert.IsNotNull(figure.getMovePointsWithoutFiltering());
        }
        [TestMethod()]
        public void getMovePointsWithoutFiltering_3_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Queen(new Point(2, 2), "Purple", board);
            var figureKing = new King(new Point(2, 2), "Purple", board);
            board.setFigure(figure, new Point(2, 2));
            board.setFigure(figureKing, new Point(2, 4));
            Assert.IsNotNull(figure.getMovePointsWithoutFiltering());
        }

        [TestMethod()]
        public void getMovePointsWithoutFiltering_4_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Horse(new Point(2, 2), "Purple", board);
            var figureKing = new King(new Point(2, 2), "Purple", board);
            board.setFigure(figure, new Point(2, 2));
            board.setFigure(figureKing, new Point(3, 4));
            Assert.IsNotNull(figure.getMovePointsWithoutFiltering());
        }

        [TestMethod()]
        public void Bishop_createTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            try
            {
                var figure = new Bishop(new Point(2, 2), "Blue", board);
                Assert.IsTrue(figure.moveSet.moves.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void Horse_createTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            try
            {
                var figure = new Horse(new Point(2, 2), "Blue", board);
                Assert.IsTrue(figure.moveSet.moves.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void Rook_createTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            try
            {
                var figure = new Rook(new Point(2, 2), "Blue", board);
                Assert.IsTrue(figure.moveSet.moves.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void Queen_createTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            try
            {
                var figure = new Queen(new Point(2, 2), "Blue", board);
                Assert.IsTrue(figure.moveSet.moves.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void King_createTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            try
            {
                var figure = new King(new Point(2, 2), "Blue", board);
                Assert.IsTrue(figure.moveSet.moves.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void King_RookSwap_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new King(new Point(4, 1), "Blue", board);
            var Rook1 = new Rook(new Point(1, 1), "Blue", board);
            var Rook2 = new Rook(new Point(8, 1), "Blue", board);
            var pawn = new Pawn(new Point(8, 1), "Blue", board, new Point(0,1));

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Rook1, new Point(1, 1));
            board.setFigure(Rook2, new Point(8, 1));
            board.setFigure(pawn, new Point(2, 1));

            board.DisplayFigureMoves(figure);

        }
        [TestMethod()]
        public void King_RookSwap_overlapped_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new King(new Point(4, 1), "Blue", board);
            var Rook1 = new Rook(new Point(1, 1), "Blue", board);
            var Rook2 = new Rook(new Point(8, 1), "Blue", board);
            var pawn = new Pawn(new Point(8, 1), "Blue", board, new Point(0, 1));
            var pawn2 = new Pawn(new Point(8, 1), "Blue", board, new Point(0, 1));

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Rook1, new Point(1, 1));
            board.setFigure(Rook2, new Point(8, 1));
            board.setFigure(pawn, new Point(3, 1));
            board.setFigure(pawn2, new Point(6, 1));

            board.DisplayFigureMoves(figure);

        }
        [TestMethod()]
        public void King_RookSwap_can_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new King(new Point(4, 1), "Blue", board);
            var Rook1 = new Rook(new Point(1, 1), "Blue", board);
            var Rook2 = new Rook(new Point(8, 1), "Blue", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Rook1, new Point(1, 1));
            board.setFigure(Rook2, new Point(8, 1));

            board.DisplayFigureMoves(figure);

        }
        [TestMethod()]
        public void King_RookSwap_can_teams_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new King(new Point(4, 1), "Blue", board);
            var Rook1 = new Rook(new Point(1, 1), "Blue", board);
            var Rook2 = new Rook(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Rook1, new Point(1, 1));
            board.setFigure(Rook2, new Point(8, 1));
            board.setFigure(Quenn, new Point(3, 3));

            board.DisplayFigureMoves(figure);

        }
        [TestMethod()]
        public void King_RookSwap_can_noRooks_teams_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new King(new Point(4, 1), "Blue", board);
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(1, 1));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(3, 3));

            board.DisplayFigureMoves(figure);

        }
        [TestMethod()]
        public void Pawn_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0,1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(1, 1));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(3, 3));
            board.MoveFigure(figure, new Point(4,2));
        }
        [TestMethod()]
        public void Pawn_kill_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(1, 1));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(4, 2));
            board.MoveFigure(figure, new Point(4, 2));
        }
        [TestMethod()]
        public void Pawn_kill_back_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(1, 1));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(4, 1));
            board.MoveFigure(figure, new Point(4, 2));
        }
        [TestMethod()]
        public void Pawn_getMovePoints_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(1, 1));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(4, 1));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void Pawn_getMovePoints_doublemove_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(4, 2));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(4, 1));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void Pawn_getMovePoints_doublemove2_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(4, 3));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(4, 1));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void Pawn_getMovePoints_doublemove3_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, -1));
            var Bishop1 = new Bishop(new Point(1, 1), "Blue", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Blue", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(4, 3));
            board.setFigure(Bishop2, new Point(8, 1));
            board.setFigure(Quenn, new Point(4, 1));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void Pawn_getMovePoints_kill_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Purple", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Purple", board);
            var Quenn = new Queen(new Point(1, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 1));
            board.setFigure(Bishop1, new Point(5, 2));
            board.setFigure(Bishop2, new Point(3, 2));
            board.setFigure(Quenn, new Point(7, 7));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void Pawn_getMovePoints_kill_proxod_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));
            var Bishop1 = new Bishop(new Point(1, 1), "Purple", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 2));
            board.setFigure(Bishop1, new Point(5, 2));
            board.setFigure(Bishop2, new Point(3, 2));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void Pawn_getMovePoints_kill_proxod_down_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, -1));
            var Bishop1 = new Bishop(new Point(1, 1), "Purple", board);
            var Bishop2 = new Bishop(new Point(8, 1), "Purple", board);

            board.setFigure(figure, new Point(4, 2));
            board.setFigure(Bishop1, new Point(5, 2));
            board.setFigure(Bishop2, new Point(3, 2));
            figure.getMovePoints();
        }
        [TestMethod()]
        public void mutatePawn_Test()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            game.Players.Add("Purple");
            game.Players.Add("Blue");
            var figure = new Pawn(new Point(4, 1), "Blue", board, new Point(0, 1));

            board.setFigure(figure, new Point(2, 7));
            figure.mutatePawn(1);
            figure.mutatePawn(2);
            figure.mutatePawn(3);
            figure.mutatePawn(4);
            figure.mutatePawn(5);
            figure.mutatePawn(6);
            figure.mutatePawn(-1);
        }
    }
}