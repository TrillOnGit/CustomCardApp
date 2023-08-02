using MtgCustomCardsApp0._2.Models;

namespace MtgCustomCardsTests;

public class CardColorTests
{
    [Theory]
    [InlineData("3WU", 5)]
    [InlineData("5WBGRR", 10)]
    [InlineData("WB", 2)]
    public void ColorTest(string manaCostText, uint expected)
    {
        var testCard = new Card
        {
            CardManaCostString = manaCostText
        };

        var actual = testCard.CardCost.White + testCard.CardCost.Blue + testCard.CardCost.Black + 
                     testCard.CardCost.Red + testCard.CardCost.Green + testCard.CardCost.Colorless;
        
        
        Assert.Equal(expected, actual);
    }

    public static TheoryData<ManaCost, string> ManaCostToStringTestData => new() {
        { new ManaCost { Colorless = 1}, "1" },
        { new ManaCost { Colorless = 1, White = 2 }, "1WW" },
        { new ManaCost { White = 1, Blue = 2, Red = 1}, "WUUR"},
        { new ManaCost { }, "0"}
    };

    [Theory]
    [MemberData(nameof(ManaCostToStringTestData))]
    public void ManaCostToStringTest(ManaCost costInput, string expected)
    {
        var actual = costInput.ToString();
        
        Assert.Equal(expected, actual);

    }
    
    public static TheoryData<ManaCost, AdjustingColor> ManaCostFrameTestData => new() {
        { new ManaCost { Colorless = 1}, AdjustingColor.Colorless },
        { new ManaCost { Colorless = 1, White = 2 }, AdjustingColor.White },
        { new ManaCost { White = 1, Blue = 2, Red = 1 }, AdjustingColor.Gold}
    };

    [Theory]
    [MemberData(nameof(ManaCostFrameTestData))]
    public void FrameColor2Test(ManaCost costInput, AdjustingColor expected)
    {
        var testCard = new Card
        {
            CardCost = costInput
        };

        var actual = testCard.FrameColor;
        
        Assert.Equal(expected, actual);

    }
    
    public static TheoryData<ManaCost, AdjustingColor> ManaCostInnerTestData => new() {
        { new ManaCost { Colorless = 1}, AdjustingColor.Colorless },
        { new ManaCost { Colorless = 1, White = 2 }, AdjustingColor.White },
        { new ManaCost { White = 1, Blue = 2, Red = 1 }, AdjustingColor.Gold},
        { new ManaCost { White = 1, Blue = 2 }, AdjustingColor.Gold}
    };

    [Theory]
    [MemberData(nameof(ManaCostInnerTestData))]
    public void InnerColor2Test(ManaCost costInput, AdjustingColor expected)
    {
        var testCard = new Card
        {
            CardCost = costInput
        };

        var actual = testCard.InnerColor;
        
        Assert.Equal(expected, actual);

    }
    public class CardCountTests
    {
        [Theory]
        [InlineData("3U", 3)]
        [InlineData("12GR", 12)]
        [InlineData("3", 3)]
        public void ColorlessCountTest(string manaCostText, uint expected)
        {
            var testCard = new Card
            {
                CardManaCostString = manaCostText
            };

            var actual = testCard.ColorlessCost;
        
            Assert.Equal(expected, actual);
        }
    }
    // Old Tools
    // [Theory]
    // [InlineData("3GR", AdjustingColor.Red | AdjustingColor.Green)]
    // [InlineData("5WBGRR", AdjustingColor.Gold)]
    // public void ColorFrameTest(string manaCostText, AdjustingColor expected)
    // {
    //     var testCard = new Card
    //     {
    //         CardManaCostString = manaCostText
    //     };
    //
    //     var actual = testCard.FrameColor;
    //     
    //     Assert.Equal(expected, actual);
    // }
    //
    // [Theory]
    // [InlineData("3GR", AdjustingColor.Gold)]
    // [InlineData("5WBGRR", AdjustingColor.Gold)]
    // [InlineData("2W", AdjustingColor.White)]
    // public void ColorInnerTest(string manaCostText, AdjustingColor expected)
    // {
    //     var testCard = new Card
    //     {
    //         CardManaCostString = manaCostText
    //     };
    //
    //     var actual = testCard.InnerColor;
    //     
    //     Assert.Equal(expected, actual);
    // }
}