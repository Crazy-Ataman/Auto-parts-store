using VintageParts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Database
{
    public class PartShopDbContext : DbContext
    {
        public PartShopDbContext() : base("DBConnection") { }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<OrderedParts> OrderedParts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderedParts>().HasKey(t => new { t.Part_id, t.Order_id });

            modelBuilder.Entity<OrderedParts>()
               .HasRequired(u => u.Order)
               .WithMany(ur => ur.Parts)
               .HasForeignKey(ui => ui.Part_id);
            
            modelBuilder.Entity<OrderedParts>()
                .HasRequired(r => r.Order)
                .WithMany(ur => ur.Parts)
                .HasForeignKey(ri => ri.Order_id);
        }
    }        
}
