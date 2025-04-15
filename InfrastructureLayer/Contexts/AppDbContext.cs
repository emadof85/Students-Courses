using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Contexts
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourse");

                entity.HasIndex(e => new { e.CourseId, e.StudentId }, "IX_StudentCourse").IsUnique();

                entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .HasPrincipalKey(s => s.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Student");
            });
            /*
            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "2", Name = "Teacher", NormalizedName = "TEACHER", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "3", Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Math 101", Description = "Basic Mathematics", CreatedAt = DateTime.Now },
                new Course { Id = 2, Name = "History 201", Description = "World History", CreatedAt = DateTime.Now }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed Admin
            var admin_id = Guid.NewGuid().ToString();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = admin_id,
                    UserName = "admin@domain.com",
                    Name = "admin",
                    Address = "any address",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@domain.com",
                    NormalizedEmail = "ADMIN@DOMAIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = admin_id }
            );*/
        }
    }
}
