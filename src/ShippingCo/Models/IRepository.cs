using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Models
{
    public interface IRepository
    {
        bool AddPackage(Package package);

        void AddStops(string trackingNumber, Stop newStop);

        void deletePackage(string trackingNumber);

        void deleteStop(string trackingNumber, string location, string activity);

        bool Exists(string trackingNumber);

        Task<bool> SaveChangesAsync();

        Package GetPackageByTracking(string trackingNumber);

        IEnumerable<Package> GetAllPackage();
    }
}
