using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdscManagement.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetTeams_AspNetUsers_TeamLeadId",
                table: "AspNetTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetTeams",
                table: "AspNetTeams");

            migrationBuilder.RenameTable(
                name: "AspNetTeams",
                newName: "Teams");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetTeams_TeamLeadId",
                table: "Teams",
                newName: "IX_Teams_TeamLeadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_TeamLeadId",
                table: "Teams",
                column: "TeamLeadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_TeamLeadId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "AspNetTeams");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_TeamLeadId",
                table: "AspNetTeams",
                newName: "IX_AspNetTeams_TeamLeadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetTeams",
                table: "AspNetTeams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetTeams_AspNetUsers_TeamLeadId",
                table: "AspNetTeams",
                column: "TeamLeadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
