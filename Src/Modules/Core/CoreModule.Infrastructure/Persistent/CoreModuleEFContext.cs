using Common.Infrastructure;
using CoreModule.Domain.CategoryAgg.Models;
using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.TeacherAgg.Models;
using CoreModule.Infrastructure.Persistent.CourseAgg;
using CoreModule.Infrastructure.Persistent.UserAgg;
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
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}