using IceProducts.Shared.InputModels;

namespace IceProducts.Server.Validators.ValidationModels
{
    public class ProductValidationModel
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }
        public string Sizes { get; set; } = string.Empty;
        public ImageData ImageData { get; set; } 
    }
}
