using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MtgCustomCardsApp0._2.Data;
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
    
    [Authorize]
    public async Task<IActionResult> Library()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var cards = await _repo.GetCardsForUser(0);
        //0 as a test ID, remember to remove this
        return View(cards);
    }

    [Authorize]
    public IActionResult CardCreator()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> InsertCardToDatabase(Card card, IFormFile img)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
        card.UserId = userId;
        if (img != null && img.Length > 0)
        {
            card.CardImg = new byte[img.Length];
            img.OpenReadStream().Read(card.CardImg, 0, (int)img.Length);
        }
        await _repo.CreateCard(card);

        return RedirectToAction("Library");
    }
    
    public async Task<IActionResult> ViewCard(int id)
    {
        var card = await _repo.GetCard(id);
        return View(card);
    }
        
    public async Task<IActionResult> UpdateCard(int id)
    {
        var card = await _repo.GetCard(id);
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