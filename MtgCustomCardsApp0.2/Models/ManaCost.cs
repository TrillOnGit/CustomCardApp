namespace MtgCustomCardsApp0._2.Models;

public class ManaCost
{
    public uint Colorless { get; set; } = 0;
    public uint White { get; set; } = 0;
    public uint Blue { get; set; } = 0;
    public uint Black { get; set; } = 0;
    public uint Red { get; set; } = 0;
    public uint Green { get; set; } = 0;

    public static ManaCost Parse(string stringInput)
    {
        var convertMc = new ManaCost();
        foreach (char elem in stringInput)
        {
            if (ColorHelpers.ColorCountByChar.Keys.Contains(elem))
            {
                var action = ColorHelpers.ColorCountByChar[elem];
                action(convertMc);
            }
        }

        convertMc.Colorless = CardCostHelper.GetColorlessMana(stringInput);

        return convertMc;
    }

    public override string ToString()
    {
        var convertString = $"{(Colorless > 0 ? Colorless : "")}";
        uint[] workArr = { White, Blue, Black, Red, Green };
        char[] charArr = { 'W', 'U', 'B', 'R', 'G' };

        for (int i = 0; i < workArr.Length; i++)
        {
            if (workArr[i] != 0)
            {
                convertString += new string(Enumerable.Repeat(charArr[i], (int)workArr[i]).ToArray());
            }
        }

        return convertString != "" ? convertString : "0"; //If there's no mana cost, return 0 instead of nothing.
    }
}