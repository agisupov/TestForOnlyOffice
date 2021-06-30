using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestForOnlyOffice.Migrations
{
    public partial class First1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("f9278c77-4a5c-407b-95db-2d8736f2cd6c"), "alexmokhov@ya.ru", "Alex", "Mokhov", "123456" },
                    { new Guid("1f8e9a51-f987-4c06-b115-f031d8f941f0"), "mukhiv@ya.ru", "Vladimir", "Mukhin", "123456" },
                    { new Guid("1f459835-ea0e-4047-b7a5-250ee512d691"), "novikov@ya.ru", "Max", "Novikov", "123456" },
                    { new Guid("963fea18-f18e-48da-967a-012c8ce6c06a"), "ivanoff@ya.ru", "Ivan", "Ivanov", "123456" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
