namespace Pirita.Tests;

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
    [InlineData(6f, 5f, 2f)]
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

    [Theory]
    [InlineData(1, 3f, 2f, 0.5f)]
    [InlineData(1f, 3f, 3f, 1f)]
    [InlineData(1f, 3f, 1f, 0f)]
    [InlineData(1f, -4f, -2f, 0.6f)]
    public void InvLerp_ReturnsPercentageBetweenMinAndMax(float min, float max, float value, float expected) {
        var result = Utils.InvLerp(min, max, value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1f, 3f, 2f, 0.5f, 2f, 1.25f)]
    [InlineData(1f, 3f, 3f, 1f, 3f, 1f)]
    [InlineData(1f, 3f, 0f, 1f, 1f, 0f)]
    public void Remap_RemapsValueBetweenInputMinAndMaxToOutput(float inputMin, float inputMax, float outputMin, float outputMax, float value, float expected) {
        var result = Utils.Remap(inputMin, inputMax, outputMin, outputMax, value);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0f, 1f, 0.5f, 0.5f)]
    [InlineData(0f, 1f, -1f, 1f)]
    [InlineData(0f, 1f, 2f, 0f)]
    [InlineData(4f, 6f, 14f, 5f)]
    public void Wrap_WrapsValueBetweenMinAndMax(float min, float max, float value, float expected) {
        var result = Utils.Wrap(value, min, max);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0f, 1f, 0.5f, 0.5f)]
    [InlineData(0f, 1f, -1f, 0f)]
    [InlineData(0f, 1f, 2f, 1f)]
    public void Clamp_MaintainsValueBetweenMinAndMax(float min, float max, float value, float expected) {
        var result = Utils.Clamp(value, min, max);

        Assert.Equal(expected, result);
    }
}

