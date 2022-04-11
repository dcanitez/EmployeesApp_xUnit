using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccountNumber", "Age", "Name" },
                values: new object[] { new Guid("0fa5d0c3-0305-4bcd-9f30-adf6cccbad88"), "123-3452134543-32", 30, "Mark" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccountNumber", "Age", "Name" },
                values: new object[] { new Guid("3dd98f18-1869-4ac4-8182-ad7cd7c61eb3"), "123-9384613085-55", 28, "Evelin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
