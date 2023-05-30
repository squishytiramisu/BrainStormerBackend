using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainStormerBackend.Migrations
{
    /// <inheritdoc />
    public partial class uj4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionSteps_Brainstorms_BrainStormId",
                table: "ActionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Brainstorms_Issues_IssueId",
                table: "Brainstorms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brainstorms",
                table: "Brainstorms");

            migrationBuilder.RenameTable(
                name: "Brainstorms",
                newName: "BrainStorms");

            migrationBuilder.RenameIndex(
                name: "IX_Brainstorms_IssueId",
                table: "BrainStorms",
                newName: "IX_BrainStorms_IssueId");

            migrationBuilder.AddColumn<bool>(
                name: "IsChosen",
                table: "BrainStorms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrainStorms",
                table: "BrainStorms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionSteps_BrainStorms_BrainStormId",
                table: "ActionSteps",
                column: "BrainStormId",
                principalTable: "BrainStorms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrainStorms_Issues_IssueId",
                table: "BrainStorms",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionSteps_BrainStorms_BrainStormId",
                table: "ActionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_BrainStorms_Issues_IssueId",
                table: "BrainStorms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrainStorms",
                table: "BrainStorms");

            migrationBuilder.DropColumn(
                name: "IsChosen",
                table: "BrainStorms");

            migrationBuilder.RenameTable(
                name: "BrainStorms",
                newName: "Brainstorms");

            migrationBuilder.RenameIndex(
                name: "IX_BrainStorms_IssueId",
                table: "Brainstorms",
                newName: "IX_Brainstorms_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brainstorms",
                table: "Brainstorms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionSteps_Brainstorms_BrainStormId",
                table: "ActionSteps",
                column: "BrainStormId",
                principalTable: "Brainstorms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brainstorms_Issues_IssueId",
                table: "Brainstorms",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
