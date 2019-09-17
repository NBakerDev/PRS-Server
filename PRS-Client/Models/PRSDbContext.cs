using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_Client.Models {
    public class PRSDbContext : DbContext {

        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }

        public PRSDbContext(DbContextOptions<PRSDbContext> context) : base(context) { }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>(ent => {

                ent.HasIndex(entity => entity.PartNbr).HasName("index_PartNbr").IsUnique();
            });

            modelBuilder.Entity<User>(ent => {

                ent.HasIndex(entity => entity.Username).HasName("index_Username").IsUnique();
            });

            modelBuilder.Entity<Request>(ent => {
            ent.Property(entity => entity.DeliveryMode).HasDefaultValueSql("('Pickup')");

            ent.Property(entity => entity.Status).HasDefaultValueSql("('NEW')");

            ent.Property(entity => entity.Total).HasDefaultValueSql("(0)");

          

            });

            



            modelBuilder.Entity<RequestLine>(ent => {
                ent.Property(entity => entity.Quantity).HasDefaultValueSql("(0)");

            });

            }
       
            
    }
}
