using Microsoft.EntityFrameworkCore.Migrations;

namespace maple_syrup_api.Migrations
{
    public partial class removeignoretabFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "UserTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "UserTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
