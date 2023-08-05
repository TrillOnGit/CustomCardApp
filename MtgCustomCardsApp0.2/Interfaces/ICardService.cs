using MtgCustomCardsApp0._2.Models;

namespace MtgCustomCardsApp0._2.Interfaces;

public interface ICardService
{
    //Read
    Task<IEnumerable<Card>> GetCardsForUser(uint userId);
    
    //Create
    Task CreateCard(Card card); //Ignore cardId in implementation, increment instead
    
    //Update
    Task UpdateCard(Card card);
    
    //Delete
    Task DeleteCard(Card card);
}

