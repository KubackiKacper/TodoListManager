using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoListAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletionStatus = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoListAssignments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ToDoListAssignments",
                columns: new[] { "Id", "CompletionStatus", "CreatedDate", "Description" },
                values: new object[] { 1, false, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is example ToDo task" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoListAssignments");
        }
    }
}
