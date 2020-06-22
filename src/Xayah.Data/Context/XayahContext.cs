using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xayah.Business.Model;

namespace Xayah.Data.Context
{
   public class XayahContext : DbContext
    {
        public XayahContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Transaction> Transactions { get; set; }


        /* get XayahContext and searching all entities mapping in DbContext.
           Search for classes that inherit from IEntityTypeConfiguration for entities that are listed in XayahContext */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(XayahContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }

    }
}
