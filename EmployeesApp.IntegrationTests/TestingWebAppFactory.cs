using EmployeesApp.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<EmployeeContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase("InMemoryEmployeeTest"));

                services.AddAntiforgery(opt =>
                {
                    opt.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                    opt.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>())
                {
                    try
                    {
                        if (appContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                            appContext.Database.EnsureCreated();
                    }
                    catch (Exception)
                    {
                        //Log errors or do anything
                        throw;
                    }
                }


            });


        }
    }
}
