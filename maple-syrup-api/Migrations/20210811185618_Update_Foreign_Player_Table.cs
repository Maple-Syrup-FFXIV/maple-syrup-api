using Microsoft.EntityFrameworkCore.Migrations;

namespace maple_syrup_api.Migrations
{
    public partial class Update_Foreign_Player_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_EventId",
                table: "Players",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Events_EventId",
                table: "Players",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Events_EventId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EventId",
                table: "Players");
        }
    }
}
