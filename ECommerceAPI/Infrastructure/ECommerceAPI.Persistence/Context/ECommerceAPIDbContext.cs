﻿using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Context
{
    public class ECommerceAPIDbContext :DbContext
    {
        public ECommerceAPIDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                //_ = data.State switch
                //{
                //    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                //    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow
                //};

                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }


            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
