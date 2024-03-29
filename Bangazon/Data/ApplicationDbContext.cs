﻿using System;
using System.Collections.Generic;
using System.Text;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Order> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related order when OrderProducts entry is removed
            modelBuilder.Entity<Order> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Order)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<Product> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            // Restrict deletion of related product when OrderProducts entry is removed
            modelBuilder.Entity<Product> ()
                .HasMany (o => o.OrderProducts)
                .WithOne (l => l.Product)
                .OnDelete (DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentType> ()
                .Property (b => b.DateCreated)
                .HasDefaultValueSql ("GETDATE()");

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Admina",
                LastName = "Straytor",
                StreetAddress = "123 Infinity Way",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            ApplicationUser anotherUser = new ApplicationUser
            {
                FirstName = "Bobby",
                LastName = "Brady",
                StreetAddress = "123 Drury Lane",
                UserName = "bobby@bobby.com",
                NormalizedUserName = "BOBBY@BOBBY.COM",
                Email = "bobby@bobby.com",
                NormalizedEmail = "BOBBY@BOBBY.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794123",
                Id = "00000000-ffff-ffff-ffff-fffffffff123"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            var anotherPasswordHash = new PasswordHasher<ApplicationUser>();
            anotherUser.PasswordHash = passwordHash.HashPassword(anotherUser, "Bobby8*");
            modelBuilder.Entity<ApplicationUser>().HasData(anotherUser);

            modelBuilder.Entity<PaymentType> ().HasData (
                new PaymentType()
                {
                    PaymentTypeId = 1,
                    UserId = user.Id,
                    Description = "American Express",
                    AccountNumber = "86753095551212"
                },
                new PaymentType()
                {
                    PaymentTypeId = 2,
                    UserId = user.Id,
                    Description = "Discover",
                    AccountNumber = "4102948572991"
                },
                new PaymentType()
                {
                    PaymentTypeId = 3,
                    UserId = anotherUser.Id,
                    Description = "Chase Sapphire Reserve",
                    AccountNumber = "1234567890110"
                }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType()
                {
                    ProductTypeId = 1,
                    Label = "Sporting Goods"
                },
                new ProductType()
                {
                    ProductTypeId = 2,
                    Label = "Appliances"
                },
                new ProductType()
                {
                    ProductTypeId = 3,
                    Label = "Tools"
                },
                new ProductType()
                {
                    ProductTypeId = 4,
                    Label = "Games"
                },
                new ProductType()
                {
                    ProductTypeId = 5,
                    Label = "Music"
                },
                new ProductType()
                {
                    ProductTypeId = 6,
                    Label = "Health"
                },
                new ProductType()
                {
                    ProductTypeId = 7,
                    Label = "Outdoors"
                },
                new ProductType()
                {
                    ProductTypeId = 8,
                    Label = "Beauty"
                },
                new ProductType()
                {
                    ProductTypeId = 9,
                    Label = "Shoes"
                },
                new ProductType()
                {
                    ProductTypeId = 10,
                    Label = "Automotive"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    UserId = user.Id,
                    Description = "It flies high",
                    Title = "Kite",
                    Quantity = 100,
                    Price = 2.99
                },
                new Product()
                {
                    ProductId = 2,
                    ProductTypeId = 2,
                    UserId = user.Id,
                    Description = "It rolls fast",
                    Title = "Wheelbarrow",
                    Quantity = 5,
                    Price = 29.99
                },
                new Product()
                {
                    ProductId = 3,
                    ProductTypeId = 3,
                    UserId = user.Id,
                    Description = "It cuts things",
                    Title = "Saw",
                    Quantity = 18,
                    Price = 31.49
                },
                new Product()
                {
                    ProductId = 4,
                    ProductTypeId = 3,
                    UserId = user.Id,
                    Description = "It puts holes in things",
                    Title = "Electric Drill",
                    Quantity = 12,
                    Price = 24.89
                },
                new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 3,
                    UserId = user.Id,
                    Description = "It puts things together",
                    Title = "Hammer",
                    Quantity = 32,
                    Price = 22.69
                },
                new Product()
                {
                    ProductId = 6,
                    ProductTypeId = 10,
                    UserId = anotherUser.Id,
                    Description = "They are environmentally friendly wheels",
                    Title = "Prius Wheels",
                    Quantity = 100,
                    Price = 69.99
                },
                new Product()
                {
                    ProductId = 7,
                    ProductTypeId = 10,
                    UserId = anotherUser.Id,
                    Description = "They are environmentally friendly mats for the winter",
                    Title = "Prius Winter Mats",
                    Quantity = 20,
                    Price = 99.99
                },
                new Product()
                {
                    ProductId = 8,
                    ProductTypeId = 10,
                    UserId = anotherUser.Id,
                    Description = "Environmentally friendly oil filters",
                    Title = "Prius Oil Filter",
                    Quantity = 200,
                    Price = 12.99
                },
                new Product()
                {
                    ProductId = 9,
                    ProductTypeId = 4,
                    UserId = anotherUser.Id,
                    Description = "I thought this game is free???",
                    Title = "Team Fight Tactics",
                    Quantity = 80,
                    Price = 59.99
                },
                new Product()
                {
                    ProductId = 10,
                    ProductTypeId = 4,
                    UserId = anotherUser.Id,
                    Description = "One of the best games ever made...",
                    Title = "God of War 3",
                    Quantity = 100,
                    Price = 59.99
                },
                new Product()
                {
                    ProductId = 11,
                    ProductTypeId = 9,
                    UserId = anotherUser.Id,
                    Description = "For people who play hockey",
                    Title = "Ice Skates",
                    Quantity = 120,
                    Price = 270
                }
            );

            modelBuilder.Entity<Order> ().HasData (
                new Order()
                {
                    OrderId = 1,
                    UserId = user.Id,
                    PaymentTypeId = null
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct()
                {
                    OrderProductId = 1,
                    OrderId = 1,
                    ProductId = 1
                }
            );

            modelBuilder.Entity<OrderProduct> ().HasData (
                new OrderProduct()
                {
                    OrderProductId = 2,
                    OrderId = 1,
                    ProductId = 2
                }
            );
        }

    }
}