using System.Data;
using MtgCustomCardsApp0._2.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            "INSERT INTO CardData (CardName, userID, CardImg, CARDTEXT, CardFlavorText, CardIllustrator, cardRARITY, cardTYPE, " +
            "cardSUBTYPE, cardPOWER, cardTOUGHNESS, ISLEGENDARY, W, U, B, R, G, C) VALUES (@name, @userId, @cardImg, @text, @flavor, " +
            "@illustrator, @rarity, @type, @subType, @power, @toughness, @isLegendary, @white, @blue, @black, " +
            "@red, @green, @colorless);",
            new
            {
                name = card.Name, text = card.CardText, userId = card.UserId, cardImg = card.CardImg,
                flavor = card.CardFlavorText, illustrator = card.Illustrator,
                rarity = card.Rarity, type = card.Type, subType = card.SubType, power = card.Power,
                toughness = card.Toughness, isLegendary = card.IsLegendary, white = card.CardCost.White,
                blue = card.CardCost.Blue, black = card.CardCost.Black, red = card.CardCost.Red,
                green = card.CardCost.Green, colorless = card.CardCost.Colorless
            });
    }

    public async Task<IEnumerable<Card>> GetCardsForUser(uint userId)
    {
        return await _conn.QueryAsync<Card, ManaCost, Card>(
            "SELECT CardID, UserID, CardName as Name, CardText, CardImg, CardFlavorText, CardType as Type, CardSubType as SubType, " +
            "CardPower as Power, CardToughness as Toughness, CardIllustrator as Illustrator, C as Colorless, W as White, U as Blue, B as Black, " +
            "R as Red, G as Green FROM CardData",
        (Card card, ManaCost manaCost) =>
        {
            card.CardCost = manaCost;
            return card;
        }, splitOn:
        "Colorless");
    }

    public async Task<Card> GetCard(int id)
    {
        var sql =
            @"SELECT CardID, UserID, CardName as Name, CardText, CardImg, CardFlavorText, CardType as Type, CardSubType as SubType, " +
            "CardPower as Power, CardToughness as Toughness, CardIllustrator as Illustrator, C as Colorless, W as White, U as Blue, B as Black," +
            " R as Red, G as Green FROM CardData WHERE CardID = @id LIMIT 1";
        return (await _conn.QueryAsync<Card, ManaCost, Card>(
            sql,
            (Card card, ManaCost manaCost) =>
            {
                card.CardCost = manaCost;
                return card;
            },
            new { id },
            splitOn: "Colorless")).First();
    }

    public async Task UpdateCard(Card card)
    {
        await _conn.ExecuteAsync(
            "UPDATE CardData SET CardName = @name, CardImg = @cardImg, CardType = @type, CardSubType = @subtype, CardPower = @power, " +
            "CardToughness = @toughness, CardText = @text, CardFlavorText = @flavor, CardIllustrator = @illustrator, " +
            "W = @white, U = @blue, B = @black, R = @red, G = @green, C = @colorless WHERE CardID = @id",
            new
            {
                name = card.Name, cardImg = card.CardImg, type = @card.Type, subtype = @card.SubType,
                power = @card.Power,
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