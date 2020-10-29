using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolDB.Models;

namespace SchoolDB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student{ Id = 1000, Name = "Rune-Örjan"},
                new Student{ Id = 1001, Name = "Bengt Alsterbengt"},
                new Student{ Id = 1002, Name = "Gunn"}
            };
            foreach(Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{ Id = 1003, Name = "Svenska"},
                new Course{ Id = 1004, Name = "JavaScript"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var assignments = new Assignment[]
            {
                new Assignment{ Id = 1005, Name = "JS ECMAScript", Description = "Förstå skillnaden"},
                new Assignment{ Id = 1006, Name = "Litteratur", Description = "Kafka"}
            };
            foreach(Assignment a in assignments)
            {
                context.Assignments.Add(a);
            }
            context.SaveChanges();

            //var teachers = new Teacher[]
            //{
            //    new Teacher{Id = 1007, Name = "Ingrid", CourseId=1003},
            //    new Teacher{Id = 1008, Name = "Ebba", CourseId=1004}
            //};
            //foreach(Teacher t in teachers)
            //{
            //    context.Teachers.Add(t);
            //}
            //context.SaveChanges();
        }
    }
}
