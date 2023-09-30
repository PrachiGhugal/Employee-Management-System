using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly EmployeeDbContext employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await employeeDbContext.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                ID = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department
            };

            await employeeDbContext.Employees.AddAsync(employee);
            await employeeDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await employeeDbContext.Employees.FirstOrDefaultAsync(x => x.ID == id);
            if (employee != null)
            {
                var editmodel = new UpdateEmployeeViewModel()
                {
                    ID = employee.ID,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    Department = employee.Department
                };
                return View(editmodel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeViewModel edit)
        {
            var employee = await employeeDbContext.Employees.FindAsync(edit.ID);
            if (employee != null)
            {
                employee.Name = edit.Name;
                employee.Email = edit.Email;
                employee.DateOfBirth = edit.DateOfBirth;
                employee.Salary = edit.Salary;
                employee.Department = edit.Department;

                await employeeDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel delete)
        {
            var emp = await employeeDbContext.Employees.FindAsync(delete.ID);
            if (emp != null)
            {
                employeeDbContext.Employees.Remove(emp);
                await employeeDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
