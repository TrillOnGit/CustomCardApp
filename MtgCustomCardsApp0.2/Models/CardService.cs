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
            "INSERT INTO CardData (Name, CARDTEXT, CardFlavorText, Illustrator, RARITY, TYPE, " +
            "SUBTYPE, POWER, TOUGHNESS, ISLEGENDARY, W, U, B, R, G, C) VALUES (@name, @text, @flavor, " +
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
            "SELECT Name, CardText, Type, SubType, Power, Toughness FROM CardData");
    }
    
    public async Task<Card> GetCard(int id)
    {
        return await _conn.QuerySingleAsync<Card>("SELECT * FROM CardData WHERE CardId = @id", new { id = id });
    }

    public async Task UpdateCard(Card card)
    {
        await _conn.ExecuteAsync(
            "UPDATE CardData SET Name = @name, Type = @type, SubType = @subtype, Power = @power, " +
            "Toughness = @toughness, CardText = @text, CardFlavorText = @flavor, Illustrator = @illustrator, " +
            "W = @white, U = @blue, B = @black, R = @red, G = @green, C = @colorless WHERE CardId = @id",
            new {name = card.Name, type = @card.Type, subtype = @card.SubType, power = @card.Power, 
                toughness = @card.Toughness, text = @card.CardText, flavor = @card.CardFlavorText, 
                illustrator = @card.Illustrator, white = card.CardCost.White, blue = card.CardCost.Blue, 
                black = card.CardCost.Black, red = card.CardCost.Red, green = card.CardCost.Green, 
                colorless = card.CardCost.Colorless, id = card.CardId
            });
    }

    public async Task DeleteCard(Card card)
    {
        await _conn.ExecuteAsync("DELETE FROM CardData WHERE CardId = @id;", new { id = card.CardId });
    }


}