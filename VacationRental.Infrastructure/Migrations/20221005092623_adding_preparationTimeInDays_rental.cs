using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationRental.Infrastructure.Migrations
{
    public partial class adding_preparationTimeInDays_rental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreparationTimeInDays",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationTimeInDays",
                table: "Rentals");
        }
    }
}
