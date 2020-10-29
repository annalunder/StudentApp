using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SchoolDB.Data;

namespace SchoolDB.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
        public Teacher Teacher { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
