using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolDB.Models;

namespace SchoolDB.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assignments);
            //.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithOne(t => t.Course)
                .HasForeignKey<Teacher>(c => c.CourseId);

            //modelBuilder.Entity<Teacher>()
            //    .HasOne(t => t.Course)
            //    .WithOne(c => c.Teacher);
            modelBuilder.Entity<Student>().HasData(new Student { Id = 1000, Name = "Rune-Örjan" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 1001, Name = "Bengt Alsterbengt" });
            modelBuilder.Entity<Course>().HasData(new Course { Id = 1004, Name = "JavaScript" });
            modelBuilder.Entity<Course>().HasData(new Course { Id = 1005, Name = "HTML" });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 1007, Name = "Ingrid", CourseId = 1004 });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 1008, Name = "Ebba", CourseId = 1005 });
            modelBuilder.Entity<Assignment>().HasData(new Assignment { Id = 1009, Name = "JS ECMAScript", Description = "Förstå skillnaden"});
            


        }
    }
}
