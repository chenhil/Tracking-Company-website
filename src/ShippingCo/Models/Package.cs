using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShippingCo.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string TrackingNumber { get; set; }

        public string CustomerName { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserName { get; set; }

        public ICollection<Stop> Stops { get; set; }

        public string Address { get; set; }

    }
}
