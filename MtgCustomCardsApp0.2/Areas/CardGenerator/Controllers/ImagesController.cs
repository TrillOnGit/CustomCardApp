using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MtgCustomCardsApp0._2.Models.Images;
using MtgCustomCardsApp0._2.Services;

namespace MtgCustomCardsApp0._2.Areas.CardGenerator.Controllers
{
	public class ImagesController : Controller
	{
		private readonly IImageService _imageService;

		public ImagesController(IImageService imageService)
		{
			this._imageService = imageService;
		}

		[HttpGet]
		public IActionResult Upload() => this.View();
		[HttpPost]
		[RequestSizeLimit(10 * 1024 * 1024)]
		public async Task<IActionResult> Upload(IFormFile image)
		{
            await this._imageService.Process(new ImageInputModel
			{
				FileName = image.FileName,
				FileType = image.ContentType,
				Content = image.OpenReadStream(),
			});

			return this.RedirectToAction(nameof(this.Done));
		}

		public IActionResult Done() => this.View();

	}
}
