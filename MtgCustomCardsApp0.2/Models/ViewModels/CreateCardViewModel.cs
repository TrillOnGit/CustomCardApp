namespace MtgCustomCardsApp0._2.Models.ViewModels
{
    public class CreateCardViewModel
    {
        public string Name { get; set; }

        public string CardImg { get; set; }

        public string Type { get; set; }

        public string SubType { get; set; }

        public string? CardText { get; set; }

        public string? CardFlavorText { get; set; }

        public string? Illustrator { get; set; }

        public char Rarity { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }

        public bool IsLegendary { get; set; } = false;

        public string CardManaCostString { get; set; }
    }
}
