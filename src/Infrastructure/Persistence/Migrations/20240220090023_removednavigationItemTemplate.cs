using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removednavigationItemTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTemplates_ItemTemplates_ParentItemTemplateId",
                table: "ItemTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ItemTemplates_ParentItemTemplateId",
                table: "ItemTemplates");

            migrationBuilder.DropColumn(
                name: "ParentItemTemplateId",
                table: "ItemTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentItemTemplateId",
                table: "ItemTemplates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemTemplates_ParentItemTemplateId",
                table: "ItemTemplates",
                column: "ParentItemTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTemplates_ItemTemplates_ParentItemTemplateId",
                table: "ItemTemplates",
                column: "ParentItemTemplateId",
                principalTable: "ItemTemplates",
                principalColumn: "Id");
        }
    }
}
