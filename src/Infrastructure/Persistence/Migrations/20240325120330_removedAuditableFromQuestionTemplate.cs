using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removedAuditableFromQuestionTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "QuestionTemplates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "QuestionTemplates");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "QuestionTemplates");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "QuestionTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Created",
                table: "QuestionTemplates",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "QuestionTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "QuestionTemplates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "QuestionTemplates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
