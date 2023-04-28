using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdscManagement.Migrations
{
    /// <inheritdoc />
    public partial class fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_TeamLeadId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamLeadId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "TeamLeadId",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamLeadId",
                table: "Teams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeadId",
                table: "Teams",
                column: "TeamLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_TeamLeadId",
                table: "Teams",
                column: "TeamLeadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
