﻿using CoreModule.Domain.OrderAgg.Models;
using CoreModule.Query._Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace CoreModule.Query._Data;

class QueryContext : DbContext
{
    public QueryContext(DbContextOptions<QueryContext> options) : base(options)
    {

    }

    public DbSet<UserQueryModel> Users { get; set; }
    public DbSet<TeacherQueryModel> Teachers { get; set; }
    public DbSet<CourseQueryModel> Courses { get; set; }
    public DbSet<SectionQueryModel> Sections { get; set; }
    public DbSet<EpisodeQueryModel> Episodes { get; set; }
    public DbSet<CategoryQueryModel> CourseCategories { get; set; }
    public DbSet<OrderQueryModel> Orders { get; set; }
    public DbSet<OrderItemQueryModel> OrderItems { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public override int SaveChanges()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        //modelBuilder.Entity<UserQueryModel>(builder =>
        //{

        //});

        modelBuilder.Entity<TeacherQueryModel>(builder =>
        {
            builder.ToTable("Teachers");
            builder.Property(b => b.UserName)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(0);

            //builder.HasOne(b=>b.User)
            //    .WithOne()
            //    .HasForeignKey<TeacherQueryModel>(b => b.UserId);
        });

        modelBuilder.Entity<CourseQueryModel>(builder =>
        {
            builder.ToTable("Courses","course");
            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords)
                    .HasMaxLength(500)
                    .HasColumnName("MetaKeyWords");

                config.Property(b => b.Canonical)
                    .HasMaxLength(500)
                    .HasColumnName("Canonical");
            });
        });

        //modelBuilder.Entity<SectionQueryModel>(builder =>
        //{
        //    builder.ToTable("Sections", "course");
        //});

        //modelBuilder.Entity<EpisodeQueryModel>(builder =>
        //{

        //});

        modelBuilder.Entity<CategoryQueryModel>(builder =>
        {

        });

        base.OnModelCreating(modelBuilder);
    }
}