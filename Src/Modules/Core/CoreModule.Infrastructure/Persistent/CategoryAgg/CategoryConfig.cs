﻿using CoreModule.Domain.CategoryAgg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.CategoryAgg;

public class CategoryConfig : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.ToTable("Categories");
        builder.HasIndex(b => b.Slug).IsUnique();

        builder.Property(b => b.Slug)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.HasMany<CourseCategory>()
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(f=>f.ParentId);
    }
}