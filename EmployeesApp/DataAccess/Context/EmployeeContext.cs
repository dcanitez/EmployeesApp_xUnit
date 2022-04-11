using EmployeesApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeesApp.DataAccess.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .HasData(
                            new Employee 
                            { 
                                Id = Guid.NewGuid(), 
                                Name = "Mark", 
                                AccountNumber = "123-3452134543-32", 
                                Age = 30 
                            }, 
                            new Employee 
                            {
                                Id = Guid.NewGuid(), 
                                Name = "Evelin", 
                                AccountNumber = "123-9384613085-55", 
                                Age = 28 
                            });
        }
    }
}
