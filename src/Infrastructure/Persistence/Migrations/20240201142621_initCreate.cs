using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistSectionTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChecklistQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChecklistSectionTemplateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistSectionTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistSectionTemplate_ChecklistSectionTemplate_ChecklistSectionTemplateId",
                        column: x => x.ChecklistSectionTemplateId,
                        principalTable: "ChecklistSectionTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UmId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AzureAdUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mobilizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ChecklistId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mobilizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mobilizations_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PartTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCheckListTemplateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartTemplates_ChecklistSectionTemplate_ItemCheckListTemplateId",
                        column: x => x.ItemCheckListTemplateId,
                        principalTable: "ChecklistSectionTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChecklistId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PartTemplateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parts_PartTemplates_PartTemplateId",
                        column: x => x.PartTemplateId,
                        principalTable: "PartTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChecklistSections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChecklistSectionTemplateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChecklistSectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChecklistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsValidated = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistSections_ChecklistSectionTemplate_ChecklistSectionTemplateId",
                        column: x => x.ChecklistSectionTemplateId,
                        principalTable: "ChecklistSectionTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistSections_ChecklistSections_ChecklistSectionId",
                        column: x => x.ChecklistSectionId,
                        principalTable: "ChecklistSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChecklistSections_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistSections_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Punches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Punches_ChecklistSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "ChecklistSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistSections_ChecklistId",
                table: "ChecklistSections",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistSections_ChecklistSectionId",
                table: "ChecklistSections",
                column: "ChecklistSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistSections_ChecklistSectionTemplateId",
                table: "ChecklistSections",
                column: "ChecklistSectionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistSections_PartId",
                table: "ChecklistSections",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistSectionTemplate_ChecklistSectionTemplateId",
                table: "ChecklistSectionTemplate",
                column: "ChecklistSectionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobilizations_ChecklistId",
                table: "Mobilizations",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ChecklistId",
                table: "Parts",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartId",
                table: "Parts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartTemplateId",
                table: "Parts",
                column: "PartTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PartTemplates_ItemCheckListTemplateId",
                table: "PartTemplates",
                column: "ItemCheckListTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Punches_SectionId",
                table: "Punches",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mobilizations");

            migrationBuilder.DropTable(
                name: "Punches");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ChecklistSections");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "PartTemplates");

            migrationBuilder.DropTable(
                name: "ChecklistSectionTemplate");
        }
    }
}
