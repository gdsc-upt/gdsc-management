using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdscManagement.Migrations
{
    /// <inheritdoc />
    public partial class Workshops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Workshop_WorkshopId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshop_AspNetUsers_TrainerId",
                table: "Workshop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshop",
                table: "Workshop");

            migrationBuilder.RenameTable(
                name: "Workshop",
                newName: "Workshops");

            migrationBuilder.RenameIndex(
                name: "IX_Workshop_TrainerId",
                table: "Workshops",
                newName: "IX_Workshops_TrainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Workshops_WorkshopId",
                table: "Participants",
                column: "WorkshopId",
                principalTable: "Workshops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_AspNetUsers_TrainerId",
                table: "Workshops",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Workshops_WorkshopId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_AspNetUsers_TrainerId",
                table: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops");

            migrationBuilder.RenameTable(
                name: "Workshops",
                newName: "Workshop");

            migrationBuilder.RenameIndex(
                name: "IX_Workshops_TrainerId",
                table: "Workshop",
                newName: "IX_Workshop_TrainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshop",
                table: "Workshop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Workshop_WorkshopId",
                table: "Participants",
                column: "WorkshopId",
                principalTable: "Workshop",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workshop_AspNetUsers_TrainerId",
                table: "Workshop",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
