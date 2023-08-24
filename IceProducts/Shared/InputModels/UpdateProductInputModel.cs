using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceProducts.Shared.InputModels
{
    internal class UpdateProductInputModel
    {
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }
        public string Sizes { get; set; } = string.Empty;
        public IBrowserFile Image { get; set; }
    }
}
