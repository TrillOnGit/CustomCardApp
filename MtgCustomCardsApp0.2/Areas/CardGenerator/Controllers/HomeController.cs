using Microsoft.AspNetCore.Mvc;
namespace MtgCustomCardsApp0._2.Areas.CardGenerator.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
}