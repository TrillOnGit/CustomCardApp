using Microsoft.AspNetCore.Mvc.RazorPages;
using MtgCustomCardsApp0._2.Models.Images;
using SixLabors.ImageSharp;

namespace MtgCustomCardsApp0._2.Services
{
	public class ImageService : IImageService
	{
		private const int ThumbnailWidth = 300;
		private const int FullScreenWidth = 1000;
		public async Task Process(ImageInputModel image)
		{
			using var imageResult = await Image.LoadAsync(image.Content);

            await this.SaveImage(imageResult, $"Original_{image.FileName}", imageResult.Width);
            await this.SaveImage(imageResult, $"Thumbnail_{image.FileName}", ThumbnailWidth);

        }

		private async Task SaveImage(Image image, string name, int resizeWidth)
        {

            var originalWidth = image.Width;
            var originalHeight = image.Height;

            if (originalWidth > resizeWidth)
            {
                originalHeight = (int)((double)resizeWidth / originalWidth * originalHeight);
                originalWidth = resizeWidth;
            }

            image.Mutate(img => img.Resize(new Size(originalWidth, originalHeight)));
            image.Metadata.ExifProfile = null;

            await image.SaveAsJpegAsync($"C:\\Users\\Harry\\Desktop\\repos\\Final Project\\CCCreatorApp\\SavedImages\\{name}", new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
            {
                Quality = 75
            });
        }

	}
}
