using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teams");
        }
    }
}
