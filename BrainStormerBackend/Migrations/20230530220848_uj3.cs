using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainStormerBackend.Migrations
{
    /// <inheritdoc />
    public partial class uj3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brainstorm_Issue_IssueId",
                table: "Brainstorm");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Projects_ProjectId",
                table: "Issue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issue",
                table: "Issue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brainstorm",
                table: "Brainstorm");

            migrationBuilder.RenameTable(
                name: "Issue",
                newName: "Issues");

            migrationBuilder.RenameTable(
                name: "Brainstorm",
                newName: "Brainstorms");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_ProjectId",
                table: "Issues",
                newName: "IX_Issues_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Brainstorm_IssueId",
                table: "Brainstorms",
                newName: "IX_Brainstorms_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                table: "Issues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brainstorms",
                table: "Brainstorms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActionSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    BrainStormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionSteps_Brainstorms_BrainStormId",
                        column: x => x.BrainStormId,
                        principalTable: "Brainstorms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActionSteps_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionSteps_BrainStormId",
                table: "ActionSteps",
                column: "BrainStormId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionSteps_IssueId",
                table: "ActionSteps",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brainstorms_Issues_IssueId",
                table: "Brainstorms",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brainstorms_Issues_IssueId",
                table: "Brainstorms");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "ActionSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brainstorms",
                table: "Brainstorms");

            migrationBuilder.RenameTable(
                name: "Issues",
                newName: "Issue");

            migrationBuilder.RenameTable(
                name: "Brainstorms",
                newName: "Brainstorm");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_ProjectId",
                table: "Issue",
                newName: "IX_Issue_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Brainstorms_IssueId",
                table: "Brainstorm",
                newName: "IX_Brainstorm_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issue",
                table: "Issue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brainstorm",
                table: "Brainstorm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brainstorm_Issue_IssueId",
                table: "Brainstorm",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Projects_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
