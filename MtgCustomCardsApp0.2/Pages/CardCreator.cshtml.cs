using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MtgCustomCardsApp0._2.Data;
using MtgCustomCardsApp0._2.Models;

namespace MtgCustomCardsApp0._2.Pages
{
    public class CardCreatorModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public CardCreatorModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Card CreateCardRequest { get; set; }


        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Convert ViewModel to DomainModel
            var cardDomainModel = new Card
            {
                Name = CreateCardRequest.Name,
                Power = CreateCardRequest.Power,
                Toughness = CreateCardRequest.Toughness,
                Type = CreateCardRequest.Type,
                SubType = CreateCardRequest.SubType,
                Rarity = CreateCardRequest.Rarity,
                IsLegendary = CreateCardRequest.IsLegendary,
                CardManaCostString = CreateCardRequest.CardManaCostString,
                CardImg = CreateCardRequest.CardImg,
                CardText = CreateCardRequest.CardText,
                CardFlavorText = CreateCardRequest.CardFlavorText,
                Illustrator = CreateCardRequest.Illustrator
            };

            dbContext.CardData.Add(cardDomainModel);
            dbContext.SaveChanges();
        }
    }
}
