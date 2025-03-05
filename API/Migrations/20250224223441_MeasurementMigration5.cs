using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class MeasurementMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserMeasurements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMeasurements_UserId1",
                table: "UserMeasurements",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_UserMeasurements_UserId1",
                table: "UserMeasurements");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserMeasurements");
        }
    }
}
