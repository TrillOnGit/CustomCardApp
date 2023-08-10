using MtgCustomCardsApp0._2.Models.Images;

namespace MtgCustomCardsApp0._2.Services
{
	public interface IImageService
	{

		public Task Process(ImageInputModel image);

	}
}
