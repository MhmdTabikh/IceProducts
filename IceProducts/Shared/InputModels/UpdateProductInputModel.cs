namespace IceProducts.Shared.InputModels
{
    public class UpdateProductInputModel
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }
        public string Sizes { get; set; } = string.Empty;
        public ImageData? ImageData { get; set; }
    }
}
