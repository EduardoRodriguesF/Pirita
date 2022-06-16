using Microsoft.Xna.Framework;
using Xunit;

namespace Pirita.Tests {
    public class UtilsTests {
        [Theory]
        [InlineData(0, 1, 0.5f, 0.5f)]
        [InlineData(1, 4, 0.25f, 1.25f)]
        [InlineData(4, 2, 0.75f, 3.25f)]
        [InlineData(-4, -2, 0.75f, -3.25f)]
        public void Approach_ApproachesFromToByAmount(float from, float to, float amount, float expected) {
            var result = Utils.Approach(from, to, amount);
            
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0.25f, 0.5f)]
        [InlineData(-3f, -2f, 5f)]
        public void Approach_StopAtTo(float from, float to, float amount) {
            var result = Utils.Approach(from, to, amount);
            
            Assert.Equal(to, result);
        }

        [Theory]
        [InlineData(0, 1, 0.5f, 0.5f)]
        [InlineData(1, 4, 0.25f, 1.75f)]
        [InlineData(4, 2, 1f, 2f)]
        [InlineData(1, 4, 2f, 7f)]
        public void Lerp_LerpsFromToByAmount(float from, float to, float amount, float expected) {
            var result = Utils.Lerp(from, to, amount);
            
            Assert.Equal(expected, result);
        }
    }
}