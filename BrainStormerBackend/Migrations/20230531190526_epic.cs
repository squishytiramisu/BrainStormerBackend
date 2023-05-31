using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainStormerBackend.Migrations
{
    /// <inheritdoc />
    public partial class epic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionSteps_BrainStorms_BrainStormId",
                table: "ActionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionSteps_Issues_IssueId",
                table: "ActionSteps");

            migrationBuilder.DropIndex(
                name: "IX_ActionSteps_IssueId",
                table: "ActionSteps");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "ActionSteps");

            migrationBuilder.AlterColumn<int>(
                name: "BrainStormId",
                table: "ActionSteps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionSteps_BrainStorms_BrainStormId",
                table: "ActionSteps",
                column: "BrainStormId",
                principalTable: "BrainStorms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionSteps_BrainStorms_BrainStormId",
                table: "ActionSteps");

            migrationBuilder.AlterColumn<int>(
                name: "BrainStormId",
                table: "ActionSteps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "ActionSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActionSteps_IssueId",
                table: "ActionSteps",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionSteps_BrainStorms_BrainStormId",
                table: "ActionSteps",
                column: "BrainStormId",
                principalTable: "BrainStorms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionSteps_Issues_IssueId",
                table: "ActionSteps",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
