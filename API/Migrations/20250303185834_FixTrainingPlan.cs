using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class FixTrainingPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingPlans_TrainingPlanId",
                table: "Trainings");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingPlans_TrainingPlanId",
                table: "Trainings",
                column: "TrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_TrainingPlans_TrainingPlanId",
                table: "Trainings");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_TrainingPlans_TrainingPlanId",
                table: "Trainings",
                column: "TrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
