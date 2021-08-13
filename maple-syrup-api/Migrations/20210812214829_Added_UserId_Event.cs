using Microsoft.EntityFrameworkCore.Migrations;

namespace maple_syrup_api.Migrations
{
    public partial class Added_UserId_Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_EventRequirements_EventId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EventId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_EventRequirements_EventRequirementId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EventRequirementId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_EventId",
                table: "Players",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_EventRequirements_EventId",
                table: "Players",
                column: "EventId",
                principalTable: "EventRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
