using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportsCollaborationAPI.Migrations
{
    public partial class FixedMigition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Notes",
                newName: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Notes",
                newName: "ReportId");
        }
    }
}
