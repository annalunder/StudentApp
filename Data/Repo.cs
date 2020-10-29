using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolDB.Models;

namespace SchoolDB.Data
{
    public class Repo
    {
        public List<Student> students = new List<Student>
        {
            new Student{ Id = 1000, Name = "Rune-Örjan"},
            new Student{ Id = 1001, Name = "Bengt Alsterbengt"},
            new Student{ Id = 1002, Name = "Gunn"}
        };
        public List<Course> courses = new List<Course>
        {
            new Course{ Id = 1003, Name = "Svenska"},
            new Course{ Id = 1004, Name = "JavaScript"}
        };
        public List<Assignment> assignments = new List<Assignment>
        {
            new Assignment{ Id = 1005, Name = "JS ECMAScript", Description = "Förstå skillnaden"},
            new Assignment{ Id = 1006, Name = "Litteratur", Description = "Kafka"}
        };
        public List<Teacher> teachers = new List<Teacher>
        {
            new Teacher{Id = 1007, Name = "Ingrid", CourseId=1003},
            new Teacher{Id = 1008, Name = "Ebba", CourseId=1004}
        };
        
    }
}
