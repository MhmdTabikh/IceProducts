using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceProducts.Shared.InputModels
{
    public class UpdateProductInputModel
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }
        public string Sizes { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
    }
}
