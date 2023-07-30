using MtgCustomCardsApp0._2.Models;

namespace MtgCustomCardsTests;

public class CardColorTests
{
    [Theory]
    [InlineData("3WU", Color.White | Color.Blue)]
    [InlineData("5WBGRR", Color.White | Color.Black | Color.Red | Color.Green)]
    public void ColorTest(string manaCostText, Color expected)
    {
        var testCard = new Card
        {
            CardManaCost = manaCostText
        };

        var actual = testCard.ColorsPresent;
        
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("3GR", AdjustingColor.Red | AdjustingColor.Green)]
    [InlineData("5WBGRR", AdjustingColor.Gold)]
    public void ColorFrameTest(string manaCostText, AdjustingColor expected)
    {
        var testCard = new Card
        {
            CardManaCost = manaCostText
        };

        var actual = testCard.FrameColor;
        
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("3GR", AdjustingColor.Gold)]
    [InlineData("5WBGRR", AdjustingColor.Gold)]
    public void ColorInnerTest(string manaCostText, AdjustingColor expected)
    {
        var testCard = new Card
        {
            CardManaCost = manaCostText
        };

        var actual = testCard.InnerColor;
        
        Assert.Equal(expected, actual);
    }
}

public class CardCountTests
{
    [Theory]
    [InlineData("3GR", 3)]
    [InlineData("12GR", 12)]
    public void ColorlessCountTest(string manaCostText, uint expected)
    {
        var testCard = new Card
        {
            CardManaCost = manaCostText
        };

        var actual = testCard.ColorlessCost;
        
        Assert.Equal(expected, actual);
    }
}