using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Domain.AcademicSessions;
using SchoolAttendance.Domain.SchoolClasses;
using SchoolAttendance.Domain.Schools;
using SchoolAttendance.Domain.Sections;
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
    public DbSet<AcademicSession> AcademicSessions => Set<AcademicSession>();
    public DbSet<SchoolClass> SchoolClasses => Set<SchoolClass>();
    public DbSet<Section> Sections => Set<Section>();

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
        modelBuilder.Entity<AcademicSession>().HasOne<Branch>().WithMany().HasForeignKey(session => session.BranchId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<AcademicSession>()
        .HasIndex(session => new
        {
            session.BranchId,
            session.Name
        }).IsUnique();
        modelBuilder.Entity<SchoolClass>().HasOne<Branch>().WithMany().HasForeignKey(session => session.BranchId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<SchoolClass>().HasIndex(session => new
        {
            session.BranchId,
            session.Name
        }).IsUnique();
        modelBuilder.Entity<Section>().HasOne<SchoolClass>().WithMany().HasForeignKey(session => session.SchoolClassId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Section>().HasIndex(session => new
        {
            session.SchoolClassId,
            session.Name
        }).IsUnique();
    }
}