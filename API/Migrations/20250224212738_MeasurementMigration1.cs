using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class MeasurementMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurement_Users_UserId",
                table: "UserMeasurement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMeasurement",
                table: "UserMeasurement");

            migrationBuilder.RenameTable(
                name: "UserMeasurement",
                newName: "UserMeasurements");

            migrationBuilder.RenameIndex(
                name: "IX_UserMeasurement_UserId",
                table: "UserMeasurements",
                newName: "IX_UserMeasurements_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMeasurements",
                table: "UserMeasurements",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMeasurements",
                table: "UserMeasurements");

            migrationBuilder.RenameTable(
                name: "UserMeasurements",
                newName: "UserMeasurement");

            migrationBuilder.RenameIndex(
                name: "IX_UserMeasurements_UserId",
                table: "UserMeasurement",
                newName: "IX_UserMeasurement_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMeasurement",
                table: "UserMeasurement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurement_Users_UserId",
                table: "UserMeasurement",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
