using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdscManagement.Migrations
{
    /// <inheritdoc />
    public partial class Teams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetTeams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TeamLeadId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MembersCount = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetTeams_AspNetUsers_TeamLeadId",
                        column: x => x.TeamLeadId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetTeams_TeamLeadId",
                table: "AspNetTeams",
                column: "TeamLeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetTeams");
        }
    }
}
