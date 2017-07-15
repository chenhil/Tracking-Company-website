using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Models
{
    public class ContextSeedData
    {
        private PackageContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<Users> _userManager;

        public ContextSeedData(PackageContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

         public async Task EnsureSeedData()
        {
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if(adminRole == null)
            {
                adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
            }


            if (await _userManager.FindByEmailAsync("lazyman1992@gmail.com") == null)
            {
                var user = new Users()
                {
                    UserName = "lazyman1992",
                    Email = "lazyman1992"
                };
                var adminResult = await _userManager.CreateAsync(user, "@Sand071292");
            }


            if (await _userManager.FindByEmailAsync("chn.hillman@gmail.com") == null)
            {
                var user = new Users()
                {
                    UserName = "chenhil",
                    Email = "chn.hillman@gmail.com"
                };
                var adminResult = await _userManager.CreateAsync(user, "@Sand071292");

                //Add user to the selected Roles
                if(adminResult.Succeeded)
                {
                    var result = await _userManager.AddToRoleAsync(user,"Admin");
                }
            }

            if(!_context.Packages.Any())
            {
                var FirstPackage = new Package()
                {
                    DateCreated = DateTime.UtcNow,
                    CustomerName = "Hillman Chen",
                    TrackingNumber = "123456ABC",
                    Address = "2539 SE 105th AVE Portland OR",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Location = "Portland, OR", Date = new DateTime(2016,10,9), Activity="Mailed out"},
                        new Stop() { Location = "Seattle, WA", Date = new DateTime(2016,10,12), Activity="On ship"},
                        new Stop() { Location = "Hong Kong", Date = new DateTime(2016,10,20), Activity="Arrived at Seaport"}
                    }
                };

                _context.Packages.Add(FirstPackage);

                _context.Stops.AddRange(FirstPackage.Stops);


                var SecondPackage = new Package()
                {
                    DateCreated = DateTime.UtcNow,
                    CustomerName = "Jenny Chen",
                    TrackingNumber = "123456DEF",
                    Address = "",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Location = "Portland, OR", Date = new DateTime(2016,10,9), Activity="Mailed out"},
                        new Stop() { Location = "Oakland, CA", Date = new DateTime(2016,10,12), Activity="On ship"},
                        new Stop() { Location = "San Fransico, CA", Date = new DateTime(2016,10,20), Activity="Arrived at Seaport"}
                    }
                };

                _context.Packages.Add(SecondPackage);

                _context.Stops.AddRange(SecondPackage.Stops);

                await _context.SaveChangesAsync();
            }
        }
    }
}
