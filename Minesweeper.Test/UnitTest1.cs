using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGEnerate2x2Grid()
        {
            var game = new Game();
            var tl = game.GenerateGrid(2, 2);
            Assert.IsNotNull(tl.Right);
            Assert.IsNotNull(tl.Bottom);
            Assert.IsNotNull(tl.Bottom.Right);
            Assert.AreEqual(tl.Right.Bottom, tl.Bottom.Right);

            Assert.IsNull(tl.Left);

        }
    }
}
