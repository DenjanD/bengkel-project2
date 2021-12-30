#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyek2_Bengkel.Models;

namespace Proyek2_Bengkel.Data
{
    public class Proyek2_BengkelContext : DbContext
    {
        public Proyek2_BengkelContext (DbContextOptions<Proyek2_BengkelContext> options)
            : base(options)
        {
        }

        public DbSet<Proyek2_Bengkel.Models.Teller> Teller { get; set; }

        public DbSet<Proyek2_Bengkel.Models.Customer> Customer { get; set; }

        public DbSet<Proyek2_Bengkel.Models.PartCategory> PartCategory { get; set; }

        public DbSet<Proyek2_Bengkel.Models.Supplier> Supplier { get; set; }

        public DbSet<Proyek2_Bengkel.Models.SparePart> SparePart { get; set; }

        public DbSet<Proyek2_Bengkel.Models.Sale> Sale { get; set; }

        public DbSet<Proyek2_Bengkel.Models.SaleDetail> SaleDetail { get; set; }

        public DbSet<Proyek2_Bengkel.Models.ServiceCategory> ServiceCategory { get; set; }

        public DbSet<Proyek2_Bengkel.Models.Service> Service { get; set; }

        public DbSet<Proyek2_Bengkel.Models.ServiceDetail> ServiceDetail { get; set; }
    }
}
