using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Model
{
    public class Purchase
    {
        public string? DisplayID { get; set; }
        public string? SupplierName { get; set; }
        public string? Date { get; set; }
        public string? Total { get; set; }
        public string? Description { get; set; }
    }
}
