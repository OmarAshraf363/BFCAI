using Banha_UniverCity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Banha_UniverCity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }
        public DbSet<CourseCurriculum> CourseCurricula { get; set; }
        public DbSet<Department> Departments { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // تعريف العلاقة بين Course وInstructor
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.CoursesTaught)
                .HasForeignKey(c => c.InstructorId);

            // تعريف العلاقة بين Enrollment وStudent
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.StudentId);

            // تعريف العلاقة بين Course وEnrollment
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID).OnDelete(DeleteBehavior.NoAction);
            ;

            // تعريف العلاقة بين Course وCourseVideo
            modelBuilder.Entity<CourseVideo>()
                .HasOne(cv => cv.Course)
                .WithMany(c => c.CourseVideos)
                .HasForeignKey(cv => cv.CourseID);

            // تعريف العلاقة بين Course وCourseCurriculum
            modelBuilder.Entity<CourseCurriculum>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.CourseCurricula)
                .HasForeignKey(cc => cc.CourseID);
        }
    }
}

