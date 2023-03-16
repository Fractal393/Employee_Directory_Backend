using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDirectoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class login : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "preferredName",
                table: "Employees",
                newName: "PreferredName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferredName",
                table: "Employees",
                newName: "preferredName");
        }
    }
}
