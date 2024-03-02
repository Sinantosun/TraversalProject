using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccsesLayer.Migrations
{
    public partial class migarion_entity_add_colmns_datetime_boold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChangePasswordEveryThreeMonthsIsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChangePasswordDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThreeMonthsLaterPasswordDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangePasswordEveryThreeMonthsIsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastChangePasswordDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThreeMonthsLaterPasswordDate",
                table: "AspNetUsers");
        }
    }
}
