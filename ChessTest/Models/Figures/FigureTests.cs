using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Core;

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
        public void clearNonSecuritedPointsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getMovePointsWithoutFilteringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getMovePointsTest()
        {
            Assert.Fail();
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
        public void pointMovableTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveTest()
        {
            Assert.Fail();
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
        public void IsPatternMoveValidTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void setAvalibleMovePointsTest()
        {
            var game = new Game();
            var board = new Board(8, 8, game, false);
            var figure = new Pawn(new Point(2, 2), "Blue", board, new Point(1,0));

            figure.setAvalibleMovePoints(); 

            Assert.IsNotNull(figure.getMovePoints);
        }
    }
}