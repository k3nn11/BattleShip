using BattleShip;
using BattleShip.Field;
using System;

namespace BattleShipTest.ShipFuctinalityTest
{
    [TestFixture]
    public class ShipsTest
    {  
        [TestCase(3)]

        public void MilitaryPropertyTest(int length)
        {
            Military military = new Military(length);
            Assert.Multiple(() =>
            {
                Assert.That(military.Direction, Is.AnyOf(typeof(Direction)));
                Assert.That(military.Weapon, Is.Not.Null);
                Assert.That(military.Health, Is.Positive);
                Assert.That(military.Length, Is.GreaterThan(0));
            });
        }

       
        [TestCase(3)]
        public void MixedPropertyTest(int length)
        {
            Mixed mixed = new Mixed(length);
            Assert.Multiple(() =>
            {
                Assert.That(mixed.Direction, Is.AnyOf(typeof(Direction)));
                Assert.That(mixed.Weapon, Is.Not.Null);
                Assert.That(mixed.Maintainance, Is.Not.Null);
                Assert.That(mixed.Health, Is.Positive);
                Assert.That(mixed.Length, Is.GreaterThan(0));
            });
        }

      
        [TestCase(1)]
        public void AuxilliaryProperty_Test(int length)
        {
            var auxilliary = new Auxilliary(length);
            Assert.Multiple(() =>
            {
                Assert.That(auxilliary.Direction, Is.AnyOf(typeof(Direction)));
                Assert.That(auxilliary.Maintainance, Is.Not.Null);
                Assert.That(auxilliary.Health, Is.Positive);
                Assert.That(auxilliary.Length, Is.GreaterThan(0));
            });
        }
    }
}
