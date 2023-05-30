using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainStormerBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brainstorm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visibility = table.Column<bool>(type: "bit", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brainstorm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brainstorm_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brainstorm_IssueId",
                table: "Brainstorm",
                column: "IssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brainstorm");
        }
    }
}
