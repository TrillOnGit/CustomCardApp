using Microsoft.AspNetCore.Mvc;
using MtgCustomCardsApp0._2.Interfaces;

namespace MtgCustomCardsApp0._2.Areas.CardGenerator.Controllers;

public class CardController : Controller
{
    private readonly ICardService _card;

    public CardController(ICardService card)
    {
        this._card = card;
    }

    // public IActionResult Index()
    // {
    //     var 
    // }
}