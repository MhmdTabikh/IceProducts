using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace IceProducts.Shared.InputModels
{
    public class ProductInputModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SmallDescription { get; set; }
        [Required]
        public string LongDescription { get; set; }

        public string Sizes { get; set; } = string.Empty;

        [Required]
        public IBrowserFile Image { get; set; }
    }
}
