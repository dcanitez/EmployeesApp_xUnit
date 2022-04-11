using EmployeesApp.Contracts;
using EmployeesApp.Controllers;
using EmployeesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesApp.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeRepository> mockRepository;
        private readonly EmployeesController controller;
        public EmployeesControllerTests()
        {
            mockRepository = new Mock<IEmployeeRepository>();
            controller = new EmployeesController(mockRepository.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfEmployees()
        {
            mockRepository.Setup(repo => repo.GetAll())
                          .Returns(new List<Employee>() { new Employee(), new Employee() });

            var result=controller.Index();

            var viewResult=Assert.IsType<ViewResult>(result);
            var employees = Assert.IsType<List<Employee>>(viewResult.Model);
            Assert.Equal(2,employees.Count);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            //testing post action
            //if it returns an employee object when there is an error on the model state.
            //testing an invalid model state

            controller.ModelState.AddModelError("Name", "Name is required");
            var employee = new Employee { Age = 25, AccountNumber = "255-8547963214-41" };

            var result=controller.Create(employee);

            var viewResult=Assert.IsType<ViewResult>(result);
            var testEmployee=Assert.IsType<Employee>(viewResult.Model);

            Assert.Equal(employee.AccountNumber,testEmployee.AccountNumber);
            Assert.Equal(employee.Age,testEmployee.Age);
        }

        [Fact]
        public void Create_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            controller.ModelState.AddModelError("Name", "Name is required");
            var employee = new Employee { Age = 34 };
            controller.Create(employee);

            //if the wrong employee is created on the database
            mockRepository.Verify(x=>x.CreateEmployee(It.IsAny<Employee>()),Times.Never());
            
            //Times Ones test should be failed
            //because the employee will not be created on db even for once.
            //mockRepository.Verify(x=>x.CreateEmployee(It.IsAny<Employee>()),Times.Once());
        }

        [Fact]
        public void Create_ModelStateValid_CreateEmployeeCalledOnce()
        {
            Employee emp = null;

            mockRepository.Setup(m=>m.CreateEmployee(It.IsAny<Employee>()))
                          .Callback<Employee>(x => emp = x);

            var employee = new Employee
            {
                Name = "Test Employee",
                Age = 30,
                AccountNumber = "123-5435789603-21"
            };

            controller.Create(employee);
            mockRepository.Verify(m=>m.CreateEmployee(It.IsAny<Employee>()),Times.Once());

            Assert.Equal(emp.Name,employee.Name);
            Assert.Equal(emp.Age, employee.Age);
            Assert.Equal(employee.AccountNumber,employee.AccountNumber);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            var employee = new Employee { Name = "Test Employee", Age = 55, AccountNumber = "123-4356874310-43" };

            var result=controller.Create(employee);
            var redirectToActionResult=Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
