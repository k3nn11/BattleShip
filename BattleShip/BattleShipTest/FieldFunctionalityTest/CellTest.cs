using BattleShip.Field;
using System;

namespace BattleShipTest.FieldFunctionalityTest
{
    [TestFixture]
    public class CellTest
    {
        [Test]
        public void CellPropertyTest()
        {
            Type type = typeof(Cell);
            var XField = type.GetProperty("X");
            var YField = type.GetProperty("Y");
            Assert.Multiple(() =>
            {
                Assert.That(XField.CanWrite, Is.False);
                Assert.That(YField.CanWrite, Is.False);
            });




        }
    }
}
