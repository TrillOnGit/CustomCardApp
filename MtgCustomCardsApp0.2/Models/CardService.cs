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
            "INSERT INTO CardData (CardName, CARDTEXT, CardFlavorText, CardIllustrator, cardRARITY, cardTYPE, " +
            "cardSUBTYPE, cardPOWER, cardTOUGHNESS, ISLEGENDARY, W, U, B, R, G, C) VALUES (@name, @text, @flavor, " +
            "@illustrator, @rarity, @type, @subType, @power, @toughness, @isLegendary, @white, @blue, @black, " +
            "@red, @green, @colorless);",
            new
            {
                name = card.Name, text = card.CardText, flavor = card.CardFlavorText, illustrator = card.Illustrator,
                rarity = card.Rarity, type = card.Type, subType = card.SubType, power = card.Power,
                toughness = card.Toughness, isLegendary = card.IsLegendary, white = card.CardCost.White, 
                blue = card.CardCost.Blue, black = card.CardCost.Black, red = card.CardCost.Red, 
                green = card.CardCost.Green, colorless = card.CardCost.Colorless
            });
    }

    public async Task<IEnumerable<Card>> GetCardsForUser(uint userId)
    {
        return await _conn.QueryAsync<Card>(
            "SELECT CardID, CardName as Name, CardText, CardType as Type, CardSubType as SubType, CardPower as Power, CardToughness as Toughness FROM CardData");
    }
    
    public async Task<Card> GetCard(int id)
    {
        return await _conn.QuerySingleAsync<Card>("SELECT * FROM CardData WHERE CardID = @id", new { id });
    }

    public async Task UpdateCard(Card card)
    {
        await _conn.ExecuteAsync(
            "UPDATE CardData SET CardName = @name, CardType = @type, CardSubType = @subtype, CardPower = @power, " +
            "CardToughness = @toughness, CardText = @text, CardFlavorText = @flavor, CardIllustrator = @illustrator, " +
            "W = @white, U = @blue, B = @black, R = @red, G = @green, C = @colorless WHERE CardID = @id",
            new {name = card.Name, type = @card.Type, subtype = @card.SubType, power = @card.Power, 
                toughness = @card.Toughness, text = @card.CardText, flavor = @card.CardFlavorText, 
                illustrator = @card.Illustrator, white = card.CardCost.White, blue = card.CardCost.Blue, 
                black = card.CardCost.Black, red = card.CardCost.Red, green = card.CardCost.Green, 
                colorless = card.CardCost.Colorless, id = card.CardId
            });
    }

    public async Task DeleteCard(Card card)
    {
        await _conn.ExecuteAsync("DELETE FROM CardData WHERE CardID = @id;", new { id = card.CardId });
    }
}