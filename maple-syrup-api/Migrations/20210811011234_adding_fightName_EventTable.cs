using Microsoft.EntityFrameworkCore.Migrations;

namespace maple_syrup_api.Migrations
{
    public partial class adding_fightName_EventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DPSType",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FightName",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinILevel",
                table: "EventRequirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinLevel",
                table: "EventRequirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FightName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MinILevel",
                table: "EventRequirements");

            migrationBuilder.DropColumn(
                name: "MinLevel",
                table: "EventRequirements");

            migrationBuilder.AlterColumn<int>(
                name: "DPSType",
                table: "Players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
