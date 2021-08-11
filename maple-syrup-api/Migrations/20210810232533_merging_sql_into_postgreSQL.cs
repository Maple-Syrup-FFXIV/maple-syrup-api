using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace maple_syrup_api.Migrations
{
    public partial class merging_sql_into_postgreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequirementId",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    PreciseJob = table.Column<bool>(type: "boolean", nullable: false),
                    OnePerJob = table.Column<bool>(type: "boolean", nullable: false),
                    DPSRequiredByType = table.Column<bool>(type: "boolean", nullable: false),
                    AllowBlueMage = table.Column<bool>(type: "boolean", nullable: false),
                    DPSTypeRequirement = table.Column<List<int>>(type: "integer[]", nullable: true),
                    ClassRequirement = table.Column<List<int>>(type: "integer[]", nullable: true),
                    PerJobRequirement = table.Column<List<int>>(type: "integer[]", nullable: true),
                    PlayerLimit = table.Column<int>(type: "integer", nullable: false),
                    PlayerCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRequirements_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerName = table.Column<string>(type: "text", nullable: true),
                    Class = table.Column<int>(type: "integer", nullable: false),
                    Job = table.Column<int>(type: "integer", nullable: false),
                    DPSType = table.Column<int>(type: "integer", nullable: true),
                    EventRequirementId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_EventRequirements_EventRequirementId",
                        column: x => x.EventRequirementId,
                        principalTable: "EventRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRequirements_EventId",
                table: "EventRequirements",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_EventRequirementId",
                table: "Players",
                column: "EventRequirementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "EventRequirements");

            migrationBuilder.DropColumn(
                name: "RequirementId",
                table: "Events");
        }
    }
}
