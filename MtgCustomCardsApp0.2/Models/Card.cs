using System.ComponentModel.DataAnnotations;

namespace MtgCustomCardsApp0._2.Models;


public class Card
{
    [Key]
    public uint CardId { get; set; } = 0;
    public uint UserId { get; set; } = 0;

    public string Name { get; set; } = "Default Name";

    public string CardImg { get; set; }

    public string Type { get; set; } = "Default T";

    public string SubType { get; set; } = "Default Sub";  

    public string CardText { get; set; } = "";

    public string CardFlavorText { get; set; } = "";

    public string Illustrator { get; set; } = "";

    public char Rarity { get; set; }

    public string Power { get; set; } = "";

    public string Toughness { get; set; } = "";

    public bool IsLegendary { get; set; } = false;
    
    public string CardManaCostString { get => CardCost.ToString(); set => CardCost = ManaCost.Parse(value); }
    
    public ManaCost CardCost { get; set; } = new();
    
    public uint ColorlessCost => CardCost.Colorless;

    public AdjustingColor FrameColor => ColorHelpers.GetFrameColor(CardCost);

    public AdjustingColor InnerColor => ColorHelpers.GetInnerColor(CardCost);
}


[Flags]
public enum AdjustingColor
{
    Colorless = 0,
    White = 1,
    Blue = 1 << 1,
    Black = 1 << 2,
    Red = 1 << 3,
    Green = 1 << 4,
    Gold = 1 << 5
}

internal static class ColorHelpers
{

    internal static readonly IDictionary<char, Action<ManaCost>> ColorCountByChar = new Dictionary<char, Action<ManaCost>>()
    {
        { 'W', c => c.White += 1  },
        { 'U', c => c.Blue += 1  },
        { 'B', c => c.Black += 1  },
        { 'R', c => c.Red += 1 },
        { 'G', c => c.Green += 1  }
    };
    
    public static AdjustingColor GetFrameColor(ManaCost inputCost)
    {
        var colorCount = 0;
        var frameColor = AdjustingColor.Colorless;
        var manaCostArr = new[] { inputCost.White, inputCost.Blue, inputCost.Black, inputCost.Red, inputCost.Green };

        for (var i = 0; i < manaCostArr.Length; i++)
        {
            if (manaCostArr[i] >= 1)
            {
                colorCount++;
                frameColor |= (AdjustingColor)(1<<i);
            }
        }
        return colorCount >= 3 ? AdjustingColor.Gold : frameColor;
    }
    public static AdjustingColor GetInnerColor(ManaCost inputCost)
    {
        var colorCount = 0;
        var frameColor = AdjustingColor.Colorless;
        var manaCostArr = new[] { inputCost.White, inputCost.Blue, inputCost.Black, inputCost.Red, inputCost.Green };

        for (var i = 0; i < manaCostArr.Length; i++)
        {
            if (manaCostArr[i] >= 1)
            {
                colorCount++;
                frameColor |= (AdjustingColor)(1<<i);
            }
        }
        return colorCount >= 2 ? AdjustingColor.Gold : frameColor;
    }
}

internal static class CardCostHelper
{
    //TODO: Either give the user a warning when inputting values like "3W4R" instead of "7WR" or account for it and add.
    public static uint GetColorlessMana(string cardManaCost)
    {
        var storageString = cardManaCost.Where(char.IsDigit).Aggregate("0", (current, t) => current + t);
        return uint.Parse(storageString);
    }
}


// public static class Example
// {
//     public static void Test()
//     {
//         string userInput = "2WW";
//         var card = new Card
//         {
//             CardCost = ManaCost.Parse(userInput)
//         };
//         Console.WriteLine(card.CardCost);
//     }
// }