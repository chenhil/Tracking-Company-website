using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.ViewModels
{
    public class PackageViewModel
    {
        public string TrackingNumber { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public string CustomerName { get; set; }

        public string Address { get; set; }
    }
}
