using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MtgCustomCardsApp0._2.Interfaces;
using MtgCustomCardsApp0._2.Models;

namespace MtgCustomCardsApp0._2.Areas.CardGenerator.Controllers;

public class CardsController : Controller
{
    private readonly ICardService _repo;

    public CardsController(ICardService repo)
    {
        this._repo = repo;
    }
    public async Task<IActionResult> Library()
    {
        var cards = await _repo.GetCardsForUser(userId:0);
        //0 as a test ID, remember to remove this
        return View(cards);
    }
        
    public async Task<IActionResult> ViewCard(int cardId)
    {
        var card = await _repo.GetCard(cardId);
        return View(card);
    }
        
    public async Task<IActionResult> UpdateCard(int cardId)
    {
        var card = await _repo.GetCard(cardId);
        return View(card);
    }

    public async Task<IActionResult> UpdateCardToDatabase(Card card)
    {
        await _repo.UpdateCard(card);

        return RedirectToAction("ViewCard", new { id = card.CardId });
    }
    
    public async Task<IActionResult> DeleteCard(Card card)
    {
        await _repo.DeleteCard(card);
        return RedirectToAction("Library");
    }

}