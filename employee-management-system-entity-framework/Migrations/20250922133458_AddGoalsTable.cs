using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employee_management_system_entity_framework.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalsId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GoalsId",
                table: "Employees",
                column: "GoalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Goals_GoalsId",
                table: "Employees",
                column: "GoalsId",
                principalTable: "Goals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Goals_GoalsId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GoalsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GoalsId",
                table: "Employees");
        }
    }
}
