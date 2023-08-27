using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IceProducts.Shared.InputModels
{
    public class ProductInputModel
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }
        public string Sizes { get; set; }
        public ImageData ImageData { get; set; } = new ImageData();
    }

    public class ImageData
    {
        public string Format { get; set; } = string.Empty;
        public byte[] Image { get; set; }
    }
}
