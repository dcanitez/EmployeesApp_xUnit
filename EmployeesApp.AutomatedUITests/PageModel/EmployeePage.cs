using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.AutomatedUITests.PageModel
{
    public class EmployeePage
    {
        private readonly IWebDriver driver;
        private const string URI = "https://localhost:5001/Employees/Create";

        private IWebElement NameElement => driver.FindElement(By.Id("Name"));
        private IWebElement AgeElement => driver.FindElement(By.Id("Age"));
        private IWebElement AccountNumberElement => driver.FindElement(By.Id("AccountNumber"));
        private IWebElement CreateElement => driver.FindElement(By.Id("Create"));

        public string Title => driver.Title;
        public string Source => driver.PageSource;
        public string AccountNumberErrorMessage => driver.FindElement(By.Id("AccountNumber-error")).Text;

        public EmployeePage(IWebDriver driver)=>this.driver = driver;

        public void Navigate()=>driver.Navigate().GoToUrl(URI);

        public void PopulateName(string nameKey)=>NameElement.SendKeys(nameKey);
        public void PopulateAge(string ageKey)=>AgeElement.SendKeys(ageKey);
        public void PopulateAccountNumber(string accountNumberKey)=>AccountNumberElement.SendKeys(accountNumberKey);
        public void ClickCreate() => CreateElement.Click();
        

    }
}
