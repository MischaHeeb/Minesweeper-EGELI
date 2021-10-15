using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Test
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void SetVisited_Test()
        {
            Field field = new Field(false);

            
            Assert.AreEqual(false, field.visited);

            field.SetVisited();

            Assert.AreEqual(true, field.visited);


        }

        [TestMethod]
        public void GetRepresemtation_Test()
        {
            Field normal = new Field(false);
            Field bomb = new Field(true);


            Assert.AreEqual("_", normal.GetRepresentation());
            Assert.AreEqual("_", bomb.GetRepresentation());

            normal.SetVisited();
            bomb.SetVisited();

            Assert.AreEqual("0", normal.GetRepresentation());
            Assert.AreEqual("x", bomb.GetRepresentation());

        }

        [TestMethod]
        public void GetBombsAroundMe_Test()
        {
            Field normal = new Field(false);
            Field bomb = new Field(true);


            Assert.AreEqual(0, normal.GetBombsAroundMe());

            normal.SetTBLR(null, null, bomb, null);
            bomb.SetTBLR(null, null, null, normal);

            Assert.AreEqual(1, normal.GetBombsAroundMe());

        }
    }
}
