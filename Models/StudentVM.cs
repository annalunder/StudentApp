using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SchoolDB.Controllers;
using static SchoolDB.Controllers.StudentsController;

namespace SchoolDB.Models
{
    public class StudentVM
    {
        public List<List<Assignment>> Assignments { get; set; }
        public List<CourseStruct> CourseStruct { get; set; }
        public Student Student { get; set; }

        [Display(Name="Course")]
        public List<Course> Courses { get; set; }
       

    }
}
