using System.ComponentModel.DataAnnotations;

namespace IceProducts.Server.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string SmallDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
