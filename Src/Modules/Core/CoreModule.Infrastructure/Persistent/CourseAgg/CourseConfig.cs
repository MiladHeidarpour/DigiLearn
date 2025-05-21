using CoreModule.Domain.CourseAgg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.CourseAgg;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses", "course");
        builder.HasIndex(b => b.Slug).IsUnique();

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.ImageName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.VideoName)
            .IsRequired(false);

        builder.OwnsMany(b => b.Sections, config =>
        {
            config.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(b => b.Sections, config =>
        {
            config.ToTable("Sections", "course");
            config.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            config.OwnsMany(b => b.Episodes, eConfig =>
            {
                eConfig.ToTable("Episodes", "course");

                eConfig.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                eConfig.Property(b => b.EnglishTitle)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(100);

                eConfig.Property(b => b.VideoName)
                    .IsRequired()
                    .HasMaxLength(200);

                eConfig.Property(b => b.AttachmentName)
                    .IsRequired(false)
                    .HasMaxLength(200);


            });
        });

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
    }
}