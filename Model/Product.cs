using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Model
{

    public class Product
    {
        public string? ProductID { get; set; }
        public string? DisplayID { get; set; }
        public string? ProductName { get; set; }
        public int? UnitID { get; set; }
        public string? UnitName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Quality { get; set; }
        public string? Description { get; set; }
    }
}
