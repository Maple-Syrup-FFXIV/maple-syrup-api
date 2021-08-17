using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace maple_syrup_api.Migrations
{
    public partial class OriginalRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "OriginalClassRequirement",
                table: "EventRequirements",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "OriginalDPSTypeRequirement",
                table: "EventRequirements",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "OriginalPerJobRequirement",
                table: "EventRequirements",
                type: "integer[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalClassRequirement",
                table: "EventRequirements");

            migrationBuilder.DropColumn(
                name: "OriginalDPSTypeRequirement",
                table: "EventRequirements");

            migrationBuilder.DropColumn(
                name: "OriginalPerJobRequirement",
                table: "EventRequirements");
        }
    }
}
