using MtgCustomCardsApp0._2.Models;

namespace TestProject1;

public class UnitTest1
{
    [Theory]
    [InlineData("3WU", Color.White | Color.Blue)]
    [InlineData("5WBGRR", Color.White | Color.Black | Color.Red | Color.Green)]
    [InlineData("nons3nSe~", Color.Colorless)]
    public void ColorTest(string manaCostText, Color expected)
    {
        var testCard = new Card
        {
            CardManaCost = manaCostText
        };

        var actual = testCard.Color;
        
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("3GR", AdjustingColor.Red | AdjustingColor.Green)]
    [InlineData("5WBGRR", AdjustingColor.Gold)]
    [InlineData("nons3nSe~", AdjustingColor.Colorless)]
    public void ColorFrameTest(string manaCostText, AdjustingColor expected)
    {
        var testCard = new Card
        {
            CardManaCost = manaCostText
        };

        var actual = testCard.FrameColor;
        
        Assert.Equal(expected, actual);
    }
}