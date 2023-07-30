namespace MtgCustomCardsApp0._2.Models;

[Flags]
public enum Color
{
    Colorless = 0,
    White = 1,
    Blue = 1 << 1,
    Black = 1 << 2,
    Red = 1 << 3,
    Green = 1 << 4,
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

internal class ManaCost
{
    public uint Colorless { get; set; } = 0;
    public uint White { get; set; } = 0;
    public uint Blue { get; set; } = 0;
    public uint Black { get; set; } = 0;
    public uint Red { get; set; } = 0;
    public uint Green { get; set; } = 0;
}

internal static class ColorHelpers
{
    private static readonly IDictionary<char, Color> ColorsByChar = new Dictionary<char, Color>()
    {
        { 'W', Color.White },
        { 'U', Color.Blue },
        { 'B', Color.Black },
        { 'R', Color.Red },
        { 'G', Color.Green }
    };

    public static Color GetCardColorFromManaCost(string cardManaCost)
    {
        var color = Color.Colorless;
        foreach (KeyValuePair<char, Color> kv in ColorsByChar)
        {
            if (cardManaCost.Contains(kv.Key))
            {
                color |= kv.Value;
            }
        }

        return color;
    }

    public static AdjustingColor GetFrameColor(Color inputColor)
    {
        var colorCount = 0;
        var frameColor = AdjustingColor.Colorless;

        for (var i = 0; i < Enum.GetNames(typeof(Color)).Length; i++)
        {
            Color bitFlag = (Color)(1 << i);
            if ((inputColor & bitFlag) == bitFlag)
            {
                colorCount++;
                frameColor |= (AdjustingColor)(1 << i);
            }
        }

        if (colorCount >= 3)
        {
            return AdjustingColor.Gold;
        }

        return frameColor;
    }

    public static AdjustingColor GetInnerColor(Color inputColor)
    {
        var colorCount = 0;
        var innerColor = AdjustingColor.Colorless;

        for (int i = 0; i < Enum.GetNames(typeof(Color)).Length; i++)
        {
            Color bitFlag = (Color)(1 << i);
            if ((inputColor & bitFlag) == bitFlag)
            {
                colorCount++;
                innerColor |= (AdjustingColor)(1 << i);
            }
        }

        if (colorCount >= 2)
        {
            return AdjustingColor.Gold;
        }

        return innerColor;
    }
}

internal static class CardCostHelper
{
    //TODO: Either give the user a warning when inputting values like "3W4R" instead of "7WR" or account for it and add.
    public static uint TryGetColorlessMana(string cardManaCost)
    {
        var storageString = cardManaCost.Where(char.IsDigit).Aggregate("", (current, t) => current + t);
        return uint.Parse(storageString);
    }
}

public class Card
{
    public uint UserId { get; set; } = 0;

    public string Name { get; set; } = "Default Name";

    public string CardManaCost { get; init; } = "0";
    
    public Color ColorsPresent => ColorHelpers.GetCardColorFromManaCost(CardManaCost);

    public uint ColorlessCost => CardCostHelper.TryGetColorlessMana(CardManaCost);

    //public Uri CardImg { get; set; }

    public string Type { get; set; } = "Default T";

    public string SubType { get; set; } = "Default Sub";

    public uint CardId { get; set; } = 0;
    
    public AdjustingColor FrameColor => ColorHelpers.GetFrameColor(ColorsPresent);

    public AdjustingColor InnerColor => ColorHelpers.GetInnerColor(ColorsPresent);

    public string CardText { get; set; } = "";

    public string CardFlavorText { get; set; } = "";

    public string Illustrator { get; set; } = "";

    public char Rarity { get; set; }

    public uint Power { get; set; } = 0;

    public uint Toughness { get; set; } = 0;

    public bool IsLegendary { get; set; } = false;
}