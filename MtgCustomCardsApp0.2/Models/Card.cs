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

internal static class ColorHelpers
{
    public static IDictionary<char, Color> ColorsByChar = new Dictionary<char, Color>()
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

        for (int i = 0; i < Enum.GetNames(typeof(Color)).Length; i++)
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
}

public class Card
{
    public uint userId { get; set; }
    
    public string CardName { get; set; }
    
    public string CardManaCost { get; set; }

    public Color Color => ColorHelpers.GetCardColorFromManaCost(CardManaCost);

    public Uri CardImg { get; set; }
    
    public string CardType { get; set; }
    
    public string CardSubType { get; set; }
    
    public uint CardId { get; set; }
    
    public AdjustingColor FrameColor { get => ColorHelpers.GetFrameColor(Color); }
    
    public string CardInnerDesignColor { get; set; }
    
    public string CardText { get; set; }
    
    public string CardFlavorText { get; set; }
    
    public string CardIllustrator { get; set; }

    public char CardRarity { get; set; }
    
    public uint CardPower { get; set; }
    
    public uint CardToughness { get; set; }
    
    public bool IsLegendary { get; set; }
}