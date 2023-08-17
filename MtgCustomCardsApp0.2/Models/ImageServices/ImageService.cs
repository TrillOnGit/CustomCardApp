using MtgCustomCardsApp0._2.Data;
using MtgCustomCardsApp0._2.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;

namespace MtgCustomCardsApp0._2.Models.ImageServices
{
    public class ImageService : IImageServce
    {
        private const int cardSizeWidth = 300;
        private const int ogImgWidth = 1024;

        private readonly IServiceScopeFactory _serviceScopeFactory;

;
        public async Task<byte[]> Process(Card image)
        {
            var tasks = new List<Task>();

            await tasks.Add(Task.Run(async () =>
            {
                using var imageResult = await Image.LoadAsync(image.CardImg);

                var ogWidth = imageResult.Width;
                var ogHeight = imageResult.Height;

                if (ogWidth > ogImgWidth)
                {
                    ogHeight = (int)((double)ogImgWidth / ogWidth * ogHeight);
                    ogWidth = ogImgWidth;
                }

                var data = this._serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

                imageResult
                    .Mutate(i => i
                    .Resize(new Size(ogWidth, ogHeight)));

                imageResult.Metadata.ExifProfile = null;

                var memoryStream = new MemoryStream();

                await imageResult.SaveAsJpegAsync(
                    image.Name, new JpegEncoder
                    {
                        Quality = 80
                    });

                return memoryStream.ToArray();
            }));
        }
    }
}
