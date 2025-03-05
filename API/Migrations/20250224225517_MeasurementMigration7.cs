using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class MeasurementMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurements_Users_UserId",
                table: "UserMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_UserMeasurements_UserId",
                table: "UserMeasurements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMeasurements");

            migrationBuilder.AlterColumn<int>(
                name: "UserId1",
                table: "UserMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements");

            migrationBuilder.AlterColumn<int>(
                name: "UserId1",
                table: "UserMeasurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserMeasurements_UserId",
                table: "UserMeasurements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurements_Users_UserId",
                table: "UserMeasurements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeasurements_Users_UserId1",
                table: "UserMeasurements",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
