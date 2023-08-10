using Humanizer;

namespace MtgCustomCardsApp0._2.Data
{
    public class ImageData
    {
        public ImageData() => this.Id = Guid.NewGuid();

        public Guid Id { get; set; }
        public string OriginalFileName { get; set; }
        public string OriginalFileType { get; set; }
        public byte[] OriginalContent { get; set; }
        public byte[] CardSizedContent { get; set; }
        public byte[] ThumbnailContent { get; set; }

    }
}
