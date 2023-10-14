using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Core.Tests
{
    [TestClass()]
    public class PointTests
    {
        [TestMethod()]
        public void getAsStringTest()
        {
            Point point = new Point(5,5);
            Assert.IsNotNull(point.getAsString());
        }
    }
}