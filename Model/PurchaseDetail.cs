using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Model
{
    public class PurchaseDetail
    {
        public string? PurchaseDetailID { get; set; }
        public string? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public string? Price { get; set; }
    }
}
