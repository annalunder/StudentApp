using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SchoolDB.Data;

namespace SchoolDB.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
