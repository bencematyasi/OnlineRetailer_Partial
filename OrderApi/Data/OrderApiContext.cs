﻿using Microsoft.EntityFrameworkCore;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class OrderApiContext : DbContext
    {
        public OrderApiContext(DbContextOptions<OrderApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne(ol => ol.Order);

            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}
