using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Model
{

    public class Product
    {
        public string? DisplayID { get; set; }
        public string? ProductName { get; set; }
        public string? Unit { get; set; }
        public string? Quantity { get; set; }

        public string? Price { get; set; }

        public string? ImageUrl { get; set; }

        public string? Quality { get; set; }

        public string? Description { get; set; }
    }
}
