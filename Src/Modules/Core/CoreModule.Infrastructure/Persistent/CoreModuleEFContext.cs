using Common.Infrastructure;
using CoreModule.Infrastructure.Persistent.UserAgg;
using CoreModules.Domain.CategoryAgg.Models;
using CoreModules.Domain.CourseAgg.Models;
using CoreModules.Domain.TeacherAgg.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent;

public class CoreModuleEFContext:BaseEfContext<CoreModuleEFContext>
{
    public CoreModuleEFContext(DbContextOptions<CoreModuleEFContext> options, IMediator mediator) : base(options, mediator)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<CourseCategory> Categories { get; set; }
    private DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        base.OnModelCreating(modelBuilder);
    }
}