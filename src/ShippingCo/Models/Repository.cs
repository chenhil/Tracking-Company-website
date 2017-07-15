using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Models
{
    public class Repository : IRepository
    {
        private PackageContext _context;

        public Repository(PackageContext context)
        {
            _context = context;
        }

        public bool AddPackage(Package package)
        {
            if (!Exists(package.TrackingNumber))
            {
                _context.Add(package);
                return true;
            }
            return false;
        }

        public void AddStops(string trackingNumber, Stop newStop)
        {
            var package = GetPackageByTracking(trackingNumber);
            if(package != null)
            {
                package.Stops.Add(newStop);

                _context.Stops.Add(newStop);
            }
        }

        public void deletePackage(string trackingNumber)
        {
            var package = GetPackageByTracking(trackingNumber);

            foreach(Stop aStop in package.Stops)
            {
                _context.Stops.Remove(aStop);
            }

            _context.Packages.Remove(package);
        }

        public void deleteStop(string trackingNumber, string location, string activity)
        {
            var package = GetPackageByTracking(trackingNumber);

            foreach(Stop aStop in package.Stops)
            {
                if(aStop.Activity == activity && aStop.Location == location)
                {
                    _context.Stops.Remove(aStop);
                }
            }
        }

        public bool Exists(string trackingNumber)
        {
            return _context.Packages.Any(t => t.TrackingNumber == trackingNumber);
        }

        public IEnumerable<Package> GetAllPackage()
        {
            return _context.Packages.ToList();
        }

        public Package GetPackageByTracking(string trackingNumber)
        {
            return _context
                .Packages
                .Include(t => t.Stops)
                .Where(t => t.TrackingNumber == trackingNumber)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        
    }
}
