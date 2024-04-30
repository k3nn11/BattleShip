using BattleShip.Field;
using BattleShip;

namespace BattleShipTest.FieldFunctionalityTest
{
    [TestFixture]
    public class PlayingFieldTest
    {
        private PlayingField playingField;

        [SetUp]
        public void Setup()
        {
            playingField = new PlayingField(3, 2);
        }

        [Test]
        public void SortFieldTest()
        {
            Type type = typeof(Ship);
            Assert.That(playingField.ShipSort, Is.Not.Null);
        }
    }
}