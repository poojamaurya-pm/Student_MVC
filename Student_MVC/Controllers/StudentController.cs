using Student_MVC.DataContext;
using Student_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_MVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        StudentContextDb _context = new StudentContextDb();
        public ActionResult Index()
        {
            var listofData = _context.students.ToList();
            return View(listofData);
        }
        public ActionResult Create()
        {
            var model = new Student
            {
                Teachers = _context.Teachers.Select(t => new SelectListItem
                {
                    Value = t.TeacherId.ToString(),
                    Text = t.TeacherName
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Student model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Student
                {
                    StudenttName = model.StudenttName,
                    Fee = model.Fee,
                    City = model.City,
                    Gender = model.Gender,
                    Hobby = model.SelectedHobbies != null ? string.Join(",", model.SelectedHobbies) : "",
                    TeacherId = model.TeacherId
                };



                _context.students.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate dropdown if there's an error
            model.Teachers = _context.Teachers.Select(t => new SelectListItem
            {
                Value = t.TeacherId.ToString(),
                Text = t.TeacherName
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employee = _context.students.Where(x => x.Id == id).FirstOrDefault();
            var model = new Student
            {
                Id = employee.Id,
                StudenttName = employee.StudenttName,
                Fee = employee.Fee,
                City = employee.City,
                Gender = employee.Gender,
                // Split the Hobby string and populate the SelectedHobbies list with the selected hobbies
                SelectedHobbies = employee.Hobby?.Split(',').ToList() ?? new List<string>(),
                TeacherId = employee.TeacherId,
                Teachers = _context.Teachers.Select(t => new SelectListItem
                {
                    Value = t.TeacherId.ToString(),
                    Text = t.TeacherName
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Student Model)
        {
            var data = _context.students.Where(x => x.Id == Model.Id).FirstOrDefault();
            if (data != null)
            {
                // Update the employee details
                data.StudenttName = Model.StudenttName;
                data.Fee = Model.Fee;
                data.City = Model.City;
                data.Gender = Model.Gender;

                // Convert List<string> to a comma-separated string for the Hobby field
                data.Hobby = string.Join(",", Model.SelectedHobbies);

                // Update TeacherId (foreign key)
                data.TeacherId = Model.TeacherId;

                // Save changes to the database
                _context.SaveChanges();
            }


            return RedirectToAction("index");
        }
        public ActionResult Details(int id)
        {
            var data = _context.students.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var data = _context.students.Where(x => x.Id == id).FirstOrDefault();
            _context.students.Remove(data);
            _context.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("index");
        }
    }
}