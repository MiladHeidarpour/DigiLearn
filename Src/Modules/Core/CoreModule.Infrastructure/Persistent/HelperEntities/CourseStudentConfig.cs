using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.HelperEntities;
using CoreModule.Infrastructure.Persistent.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistent.HelperEntities;

public class CourseStudentConfig:IEntityTypeConfiguration<CourseStudent>
{
    public void Configure(EntityTypeBuilder<CourseStudent> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne<Course>()
            .WithMany()
            .HasForeignKey(f => f.CourseId);
    }
}