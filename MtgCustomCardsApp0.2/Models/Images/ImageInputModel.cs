using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MtgCustomCardsApp0._2.Models.Images

{
	public class ImageInputModel
	{        
        public string FileName { get; set; }
		public string FileType { get; set; }
		public Stream Content { get; set; }

	}
}
