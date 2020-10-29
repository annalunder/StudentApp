using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SchoolDB.Models
{
    public class CourseVM
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public int [] SelectAssignmentArray { get; set; }
        [Display(Name="Select Assignments")]
        public List<Assignment> SelectAssignments { get; set; }
        public int[] SelectStudentsArray { get; set; }
        [Display(Name = "Select Students")]
        public List<Student> SelectStudents { get; set; }
        public Course Course { get; set; }
        //public List<Course> Courses { get; set; }
    }
}
