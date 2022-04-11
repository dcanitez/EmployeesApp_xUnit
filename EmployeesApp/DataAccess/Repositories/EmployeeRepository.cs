using EmployeesApp.Contracts;
using EmployeesApp.DataAccess.Context;
using EmployeesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesApp.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext context;

        public EmployeeRepository(EmployeeContext context)
        {
            this.context = context;
        }
        public void CreateEmployee(Employee employee)
        {
            context.Add(employee);
            context.SaveChanges();
        }

        public Employee Get(Guid id)=>context.Employees.SingleOrDefault(e=> e.Id.Equals(id));        

        public IEnumerable<Employee> GetAll() => context.Employees.ToList();
        
    }
}
