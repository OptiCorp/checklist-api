using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class checklistQuestionNotApplicable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotApplicable",
                table: "ChecklistItems");

            migrationBuilder.AddColumn<bool>(
                name: "NotApplicable",
                table: "ChecklistItemQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotApplicable",
                table: "ChecklistItemQuestions");

            migrationBuilder.AddColumn<bool>(
                name: "NotApplicable",
                table: "ChecklistItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
