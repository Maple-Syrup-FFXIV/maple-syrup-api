using Microsoft.EntityFrameworkCore.Migrations;

namespace maple_syrup_api.Migrations
{
    public partial class Replaced_Event_For_Event_Requirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_EventRequirements_EventRequirementId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Events_EventId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EventRequirementId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "EventRequirementId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_EventRequirements_EventId",
                table: "Players",
                column: "EventId",
                principalTable: "EventRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_EventRequirements_EventId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "EventRequirementId",
                table: "Players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_EventRequirementId",
                table: "Players",
                column: "EventRequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_EventRequirements_EventRequirementId",
                table: "Players",
                column: "EventRequirementId",
                principalTable: "EventRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Events_EventId",
                table: "Players",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
