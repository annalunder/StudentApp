using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;

namespace SchoolDB.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Id == id); //SingleOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                ViewData["Teacher"] = "No teacher selected to this course";
            }
            else
            { 
                ViewData["Teacher"] = teacher.Name;
            }
            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseVM courseVM = new CourseVM();

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            courseVM.Course = course;

            var assignments = await _context.Assignments.ToListAsync();

            //populate preselected multiselect of assignments to this course
            var thisCourseAssignments = await _context.Assignments.Where(a => a.Course.Id == id).ToListAsync().ConfigureAwait(false);
            courseVM.SelectAssignmentArray = thisCourseAssignments.Select(a => a.Id).ToArray();
            courseVM.SelectAssignments = assignments;   //multiselect lables

            //populate preselected multiselect of students to this course
            List<Student> allStudents = await _context.Students.ToListAsync().ConfigureAwait(false);
            var thisCourseStudents = await _context.Students.Where(s => s.StudentCourses.Any(i => i.CourseId == id)).ToListAsync().ConfigureAwait(false);
            courseVM.SelectStudentsArray = thisCourseStudents.Select(a => a.Id).ToArray();
            courseVM.SelectStudents = allStudents;   //multiselect lables


            return View(courseVM);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseVM courseVM)//[Bind("Id,Name")] Course course )
        {
            if (ModelState.IsValid)
            {
                Course course = new Course() { Id = id };

                if (id != courseVM.Id)
                {
                    return NotFound();
                }
                //course.Id = id;
                course.Name = courseVM.Course.Name;

                //Course CourseWithOldAssigments = _context.Courses.Find(id);

                if (courseVM.SelectAssignmentArray != null)
                {
                    var newAssignments = new List<Assignment>();
                    newAssignments = _context.Assignments
                        .Where(a => courseVM.SelectAssignmentArray.Contains(a.Id)).ToList();

                    course.Assignments = newAssignments;
                    //_context.Courses.Find(id).Assignments.Clear();
                    //_context.Courses.Find(id).Assignments.AddRange(newAssignments);

                }

                var oldSelectionOfStudents = await _context.StudentCourses.Where(a => a.Course.Id == id).AsNoTracking().ToListAsync().ConfigureAwait(false);
                IList<StudentCourse> courseStudentsToDeselect = new List<StudentCourse>();
                if (oldSelectionOfStudents.Count > 0 && !(oldSelectionOfStudents == null))
                {
                    for (var i = 0; i < oldSelectionOfStudents.Count; i++)
                    {
                        courseStudentsToDeselect.Add(new StudentCourse
                        {
                            StudentId = oldSelectionOfStudents[i].StudentId,
                            CourseId = id
                        });
                    }
                }
                _context.StudentCourses.RemoveRange(courseStudentsToDeselect);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                //Save the new selection of students
                if (courseVM.SelectStudentsArray != null)
                {
                    IList<StudentCourse> studentCourseToDB = new List<StudentCourse>();
                    if ((courseVM.SelectStudentsArray.Length > 0) && !(courseVM.SelectStudentsArray is null))
                    {
                        for (var i = 0; i < courseVM.SelectStudentsArray.Length; i++)
                        {
                            studentCourseToDB.Add(new StudentCourse
                            {
                                StudentId = courseVM.SelectStudentsArray[i],
                                CourseId = id
                            });
                        }
                    }
                    _context.StudentCourses.AddRange(studentCourseToDB);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }

                try
                {
                    //_context.Entry(CourseWithOldAssigments).State = EntityState.Detached;
                    _context.Update(course).DetectChanges();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            }//inside validstate


            return RedirectToAction(nameof(Index));   //View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
