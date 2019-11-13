using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam2C2P.Persistence.Migrations
{
    public partial class change_name_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "EventLog",
                newName: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventLog",
                newName: "EventID");
        }
    }
}
