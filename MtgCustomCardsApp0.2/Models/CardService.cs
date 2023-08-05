using System.Data;
using MtgCustomCardsApp0._2.Interfaces;
using Dapper;

namespace MtgCustomCardsApp0._2.Models;

public class CardService : ICardService
{
    private readonly IDbConnection _conn;

    public CardService(IDbConnection conn)
    {
        _conn = conn;
    }

    public async Task CreateCard(Card card)
    {
        await _conn.ExecuteAsync(
            "INSERT INTO CardData (CARDNAME, CARDTEXT, CardFlavorText, CardIllustrator) VALUES (@name, @text, @flavor, @illustrator);",
            new
            {
                name = card.Name, text = card.CardText, flavor = card.CardFlavorText, illustrator = card.Illustrator
            });
        await _conn.ExecuteAsync(
            "INSERT INTO CardData (cardRARITY, cardTYPE, cardSUBTYPE, cardPOWER, cardTOUGHNESS, ISLEGENDARY) VALUES " +
            "(@rarity, @type, @subType, @power, @toughness, @isLegendary);",
            new
            {
                rarity = card.Rarity, type = card.Type, subType = card.SubType, power = card.Power,
                toughness = card.Toughness, isLegendary = card.IsLegendary
            });
        await _conn.ExecuteAsync(
            "INSERT INTO CardData (W, U, B, R, G, C) VALUES " +
            "(@white, @blue, @black, @red, @green, @colorless);",
            new
            {
                white = card.CardCost.White, blue = card.CardCost.Blue, black = card.CardCost.Black, 
                red = card.CardCost.Red, green = card.CardCost.Green, colorless = card.CardCost.Colorless
            });
    }

    public async Task<IEnumerable<Card>> GetCardsForUser(uint userId)
    {
        return await _conn.QueryAsync<Card>("SELECT CardName, CardText, CardType, CardSubType, CardPower, CardToughness FROM CardData");
    }

    public Task UpdateCard(Card card)
    {
        _conn.ExecuteAsync("UPDATE CardData")
        throw new NotImplementedException();
    }

    public Task DeleteCard(uint cardId)
    {
        throw new NotImplementedException();
    }
}