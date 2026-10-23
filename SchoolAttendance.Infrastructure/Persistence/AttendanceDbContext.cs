using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Domain.Schools;
using SchoolAttendance.Domain.Students;

namespace SchoolAttendance.Infrastructure.Persistence;

public class AttendanceDbContext : DbContext
{
    public AttendanceDbContext(
        DbContextOptions<AttendanceDbContext> options
    ) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<School> Schools => Set<School>();
    public DbSet<Branch> Branches => Set<Branch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasIndex(student => student.StudentCode).IsUnique();
        modelBuilder.Entity<School>().HasIndex(school => school.SchoolCode).IsUnique();
        modelBuilder.Entity<Branch>().HasIndex(branch => new
        {
            branch.SchoolId,
            branch.BranchCode
        }).IsUnique();
        modelBuilder.Entity<Branch>().HasOne<School>().WithMany().HasForeignKey(branch => branch.SchoolId).OnDelete(DeleteBehavior.Restrict);
    }
}