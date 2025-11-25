using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RmsysStudentPortal.web.Data;
using RmsysStudentPortal.web.Models;
using RmsysStudentPortal.web.Models.Entities;

namespace RmsysStudentPortal.web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly RmsysStudentPortalDbContext dbcontext;

        public StudentsController(RmsysStudentPortalDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                Subscribed = viewModel.Subscribed,
            };

            await dbcontext.Students.AddAsync(student);
            await dbcontext.SaveChangesAsync();

            TempData["Success"] = "Student added successfully!";

            return RedirectToAction("List");  
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbcontext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbcontext.Students.FindAsync(id);

            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbcontext.Students.FindAsync(viewModel.Id);
            
            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Address = viewModel.Address;
                student.Subscribed = viewModel.Subscribed;

                await dbcontext.SaveChangesAsync();

                TempData["Success"] = "Student updated successfully!";
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await dbcontext.Students.FindAsync(id);

            if (student != null)
            {
                dbcontext.Students.Remove(student); 
                await dbcontext.SaveChangesAsync();

                TempData["Success"] = "Student deleted successfully!";
            }

            return RedirectToAction("List");
        }


    }
}
