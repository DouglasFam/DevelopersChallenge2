using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xayah.Business.Model;

namespace Xayah.Data.Mappings
{
    public class DocumentMapping : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.Id);
           
            builder.Property(d => d.FileUpload)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Documents");


          
        }
    }
}
