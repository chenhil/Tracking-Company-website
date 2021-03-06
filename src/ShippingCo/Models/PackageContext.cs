﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Models
{
    public class PackageContext : IdentityDbContext<Users>
    {
        private IConfigurationRoot _config;

        public PackageContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }   

        public DbSet<Package> Packages { get; set; }

        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:ContextConnection"]);
        }
    }
}
