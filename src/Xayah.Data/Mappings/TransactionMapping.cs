using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xayah.Business.Model;

namespace Xayah.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TRNTYPE)
                .HasColumnName("Type")
                .IsRequired();

            builder.Property(t => t.TRNAMT)
                .HasColumnName("Value")
                .IsRequired();

            builder.Property(t => t.MEMO)
                .HasColumnName("Memo")
                .IsRequired();

            builder.ToTable("Transactions");
        }
    }
}
