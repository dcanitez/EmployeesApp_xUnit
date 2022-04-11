using EmployeesApp.AutomatedUITests.PageModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesApp.AutomatedUITests
{
    public class AutomatedUITests:IDisposable
    {
        private readonly IWebDriver driver;
        private readonly EmployeePage pageModel;
        public AutomatedUITests()
        {
            driver = new ChromeDriver();
            pageModel=new EmployeePage(driver);
            pageModel.Navigate();
            
        }
       
        public void Dispose()
        {
            //use this method to close a chrome window 
            //opened by the ChromeDriver and also to dispose of it

            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Create_WhenExecuted_ReturnsCreateView()
        {          
            Assert.Equal("Create - EmployeesApp",pageModel.Title);
            Assert.Contains("Please provide a new employee data",pageModel.Source);
        }

        [Fact]
        public void Create_WrongModelData_ReturnsErrorMessage()
        {
            pageModel.PopulateName("Test Employee");
            pageModel.PopulateAge("34");
            pageModel.ClickCreate();
            Assert.Equal("Account Number is required.", pageModel.AccountNumberErrorMessage);
        }

        [Fact]
        public void Create_WhenSuccessfullyExecuted_ReturnsIndexViewWithNewEmployee()
        {
            pageModel.PopulateName("Another Test Employee");
            pageModel.PopulateAge("28");
            pageModel.PopulateAccountNumber("123-9384613085-58");
            pageModel.ClickCreate();


            Assert.Equal("Index - EmployeesApp", pageModel.Title);
            Assert.Contains("Another Test Employee", pageModel.Source);
            Assert.Contains("34", pageModel.Source);
            Assert.Contains("123-9384613085-58", pageModel.Source);
        }
    }
}
