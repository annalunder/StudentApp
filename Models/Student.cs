using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SchoolDB.Data;

namespace SchoolDB.Models
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
