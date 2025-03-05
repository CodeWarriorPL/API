using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class RenameUserId1ToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "UserMeasurements",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMeasurements_UserId1",
                table: "UserMeasurements",
                newName: "IX_UserMeasurements_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurements_Users_UserId",
                table: "UserMeasurements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurements_Users_UserId",
                table: "UserMeasurements");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserMeasurements",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserMeasurements_UserId",
                table: "UserMeasurements",
                newName: "IX_UserMeasurements_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
