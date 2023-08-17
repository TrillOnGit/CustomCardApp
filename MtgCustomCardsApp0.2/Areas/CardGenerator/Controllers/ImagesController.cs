using Microsoft.AspNetCore.Mvc;
using MtgCustomCardsApp0._2.Interfaces;

namespace MtgCustomCardsApp0._2.Areas.CardGenerator.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageServce _imageServce;

        public ImagesController(IImageServce _imageServce) => this._imageServce = _imageServce;

        [HttpGet]
        public IActionResult Upload() => this.View();
        

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile image)
        {

            await this._imageServce.Process(image.OpenReadStream());

            return this.RedirectToAction("Library");  

        }
    }
}
