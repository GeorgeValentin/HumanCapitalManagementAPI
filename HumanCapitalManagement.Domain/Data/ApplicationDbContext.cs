using HumanCapitalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Domain.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        :base(options)
    { }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Nationality> Nationalities => Set<Nationality>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<StudyProgram> StudyPrograms => Set<StudyProgram>();
    public DbSet<Degree> Degrees => Set<Degree>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<EmployeeStudyProgram> EmployeesStudyPrograms => Set<EmployeeStudyProgram>();
    public DbSet<JobTitle> JobTitles => Set<JobTitle>();
    public DbSet<EmployeeSkill> EmployeeSkills => Set<EmployeeSkill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasMany(a => a.Contracts)
            .WithOne(a => a.Employee)
            .IsRequired();

        modelBuilder.Entity<EmployeeSkill>()
            .HasKey(i => new { i.SkillID, i.EmployeeId });
        modelBuilder.Entity<EmployeeSkill>().ToTable("EmployeeSkills");

        modelBuilder.Entity<Feedback>()
            .HasOne(c => c.FromEmployee)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Feedback>()
            .HasOne(c => c.ToEmployee)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
