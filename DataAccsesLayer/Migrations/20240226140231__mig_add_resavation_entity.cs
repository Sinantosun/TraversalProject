using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccsesLayer.Migrations
{
    public partial class _mig_add_resavation_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Reservations");
        }
    }
}
