using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ToDoListAssignments",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoListAssignments",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
