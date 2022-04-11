using EmployeesApp.Contracts;
using EmployeesApp.Models;
using EmployeesApp.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeesApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository repository;
        private readonly AccountNumberValidation validation;
        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
            validation = new AccountNumberValidation();
        }
        public IActionResult Index()
        {
            var employees=repository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,AccountNumber,Age")] Employee employee)
        {
            if(!ModelState.IsValid)
                return View(employee);
            if (!validation.IsValid(employee.AccountNumber))
            {
                ModelState.AddModelError("AccountNumber", "Account Number is invalid.");
                return View(employee);
            }

            repository.CreateEmployee(employee);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
