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
        
    public async Task<IActionResult> UpdateProduct(int cardId)
    {
        Task<Card> prod = _repo.GetCard(cardId);
        if (prod == null)
        {
            return View("Index");
        }
        return View(prod);
    }

    public IActionResult UpdateProductToDatabase(Product product)
    {
        _repo.UpdateProduct(product);

        return RedirectToAction("ViewProduct", new { id = product.ProductId });
    }
        
    public IActionResult InsertProduct()
    {
        var prod = _repo.AssignCategory();
        return View(prod);
    }
        
    public IActionResult InsertProductToDatabase(Product productToInsert)
    {
        _repo.InsertProduct(productToInsert);
        return RedirectToAction("Index");
    }
        
    public IActionResult DeleteProduct(Product product)
    {
        _repo.DeleteProduct(product);
        return RedirectToAction("Index");
    }

}

    // public IActionResult Index()
    // {
    //     var 
    // }
}