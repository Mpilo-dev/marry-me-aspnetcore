using Marry_Me.EF.Configuration;
using Marry_Me.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Marry_Me.EF.Context
{
    public class MarriageSystemDbContext(DbContextOptions<MarriageSystemDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Marriage> Marriages { get; set; }
        public DbSet<Divorce> Divorces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new MarriageConfiguration());
            modelBuilder.ApplyConfiguration(new DivorceConfiguration());

        }
    }
}