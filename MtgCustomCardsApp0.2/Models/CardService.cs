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
            "INSERT INTO CardText (NAME, TEXT, FLAVOR, ILLUSTRATOR) VALUES (@name, @text, @flavor, @illustrator);",
            new
            {
                name = card.Name, text = card.CardText, flavor = card.CardFlavorText, illustrator = card.Illustrator
            });
        await _conn.ExecuteAsync(
            "INSERT INTO CardData (RARITY, TYPE, SUBTYPE, POWER, TOUGHNESS, ISLEGENDARY) VALUES " +
            "(@rarity, @type, @subType, @power, @toughness, @isLegendary);",
            new
            {
                rarity = card.Rarity, type = card.Type, subType = card.SubType, power = card.Power,
                toughness = card.Toughness, isLegendary = card.IsLegendary
            });
        await _conn.ExecuteAsync(
            "INSERT INTO Color (WHITE, BLUE, BLACK, RED, GREEN, COLORLESS) VALUES " +
            "(@white, @blue @black, @red, @green, @colorless);",
            new
            {
                white = card.CardCost.White, blue = card.CardCost.Blue, black = card.CardCost.Black, 
                red = card.CardCost.Red, green = card.CardCost.Green, colorless = card.CardCost.Colorless
            });
    }

    public async Task<IEnumerable<Card>> GetCardsForUser(uint userId)
    {
        return await _conn.QueryAsync<Card>("SELECT * FROM CardData" +
                               "JOIN CardText " +
                               "ON CardData.CardID = CardText.CardID");
    }

    public Task UpdateCard(Card card)
    {
        
        throw new NotImplementedException();
    }

    public Task DeleteCard(uint cardId)
    {
        throw new NotImplementedException();
    }
}