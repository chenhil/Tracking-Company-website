using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.ViewModels
{
    public class StopViewModel
    {
        public string Location { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Activity { get; set; }

    }
}
