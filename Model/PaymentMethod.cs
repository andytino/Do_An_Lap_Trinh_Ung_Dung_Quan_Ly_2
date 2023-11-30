using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Model
{

    public class PaymentMethod
    {
        public int? PaymentID { get; set; }
        public string? MethodName { get; set; }
    }
}
