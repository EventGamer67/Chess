using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Core;
using System.Collections;
using Chess.Tools;

namespace Chess.Models.Figures.Tests
{
    [TestClass()]
    public class ToolsTests
    {
        [TestMethod()]
        public void pointAxisValid_returnFalse_Test()
        {
            Assert.IsFalse(Tools.Tools.pointAxisValid("asd df"));
        }
        [TestMethod()]
        public void pointAxisValid_null_returnFalse_Test()
        {
            Assert.IsFalse(Tools.Tools.pointAxisValid(null));
        }
        [TestMethod()]
        public void pointAxisValid_returnTrue_Test()
        {
            Assert.IsTrue(Tools.Tools.pointAxisValid("5"));
        }
        [TestMethod()]
        public void DeepCloneHelper_returnTrue_Test()
        {
            var smth = "1234";
            var smthCopied = DeepCopyHelper.DeepCopy(smth);

            Assert.IsTrue(smth == smthCopied);
        }
        [TestMethod()]
        public void DeepCloneHelper_returnFalse_Test()
        {
            String smth = null;
            try
            {
                String smth2 = DeepCopyHelper.DeepCopy(smth);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
            }
        }
        [TestMethod()]
        public void DeepCloneHelper_notSerializible_returnFalse_Test()
        {
            nonSerializble smth = new nonSerializble("asd");
            try
            {
                nonSerializble smth2 = DeepCopyHelper.DeepCopy(smth);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
            }
        }
    }
}