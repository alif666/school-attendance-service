using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Domain.AcademicSessions;
using SchoolAttendance.Domain.Enrollments;
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
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

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
        modelBuilder.Entity<SchoolClass>().HasOne<Branch>().WithMany().HasForeignKey(schoolClass => schoolClass.BranchId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<SchoolClass>().HasIndex(schoolClass => new
        {
            schoolClass.BranchId,
            schoolClass.Name
        }).IsUnique();
        modelBuilder.Entity<Section>().HasOne<SchoolClass>().WithMany().HasForeignKey(section => section.SchoolClassId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Section>().HasIndex(section => new
        {
            section.SchoolClassId,
            section.Name
        }).IsUnique();
        modelBuilder.Entity<Enrollment>().HasOne<Student>().WithMany().HasForeignKey(enrollment => enrollment.StudentId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Enrollment>().HasOne<AcademicSession>().WithMany().HasForeignKey(enrollment => enrollment.AcademicSessionId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Enrollment>().HasOne<Section>().WithMany().HasForeignKey(enrollment => enrollment.SectionId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Enrollment>().HasIndex(enrollment => new
        {
            enrollment.StudentId,
            enrollment.AcademicSessionId,
        }).IsUnique().HasFilter("\"IsActive\"=TRUE");
    }
}
