using EmployeesApp.Models;
using System;
using System.Collections.Generic;

namespace EmployeesApp.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(Guid id);
        void CreateEmployee(Employee employee);
    }
}
