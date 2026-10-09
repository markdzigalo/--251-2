using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatAndMouse;

namespace CatAndMouse.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Player_NormalizeLocation_WrapsAroundRing()
        {
            
            Player player = new Player("Cat");
            player.Move(1, 26);  
            player.Move(27, 26); 

            Assert.AreEqual(2, player.location);
        }

        [TestMethod]
        public void Player_TracksDistanceCorrectly()
        {
            Player player = new Player("Mouse");
            player.Move(5, 26);  
            player.Move(3, 26);  
            player.Move(-2, 26); 

            Assert.AreEqual(5, player.distanceTraveled);
        }
    }
}