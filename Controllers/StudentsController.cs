using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        // Injecting the DbContext class inside Student Controller 
        // by Defining a Constructor
        private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Defining the Method for form data saving inside database
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };

            // Preparing Data
            await dbContext.Students.AddAsync(student);
            // Saving to Database
            await dbContext.SaveChangesAsync();
            return View();
        }
    
        // Getting data from Databse Using Entity Framework by dbContext

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            
            if(student is not null){

                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                 await dbContext.SaveChangesAsync();
             }

            return RedirectToAction("List", "Students");
        }
    

    // Deleting a Student Method
    [HttpPost]
    public async Task<IActionResult> Delete(Student viewModel)
    {
        var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
        if(student is not null){
            dbContext.Students.Remove(viewModel);
            await dbContext.SaveChangesAsync();
        }
        
        return RedirectToAction("List", "Students");
    }

    }
}
