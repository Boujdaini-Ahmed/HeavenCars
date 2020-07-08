using Microsoft.EntityFrameworkCore.Migrations;

namespace HeavenCars.DataAccessLayer.Migrations
{
    public partial class deletecarMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "Cars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delete",
                table: "Cars");
        }
    }
}
