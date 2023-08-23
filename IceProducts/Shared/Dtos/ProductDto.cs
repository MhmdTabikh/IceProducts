using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceProducts.Shared.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }
        public string Sizes { get; set; }
        public byte[] Image { get; set; }
    }
}
    