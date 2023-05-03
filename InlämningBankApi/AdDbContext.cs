using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InlämningBankApi
{
    public class AdDbContext : DbContext
    {
        public AdDbContext(DbContextOptions<AdDbContext> options) : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; }

        public void MigrateAndSeed(AdDbContext myContext)
        {
            myContext.Database.Migrate();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=BankApi;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            }
        }

    }
}
