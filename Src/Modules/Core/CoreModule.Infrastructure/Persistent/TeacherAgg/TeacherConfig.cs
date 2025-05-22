using CoreModule.Domain.TeacherAgg.Models;
using CoreModule.Infrastructure.Persistent.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.TeacherAgg;

public class TeacherConfig:IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.HasKey(b=>b.Id);
        builder.HasIndex(b=>b.UserName).IsUnique();
        builder.Property(b => b.UserName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Teacher>(b => b.UserId);
    }
}