using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam2C2P.Persistence.Migrations
{
    public partial class Update_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventLog",
                table: "EventLog");

            migrationBuilder.RenameTable(
                name: "EventLog",
                newName: "EventLogs");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventLogs",
                table: "EventLogs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventLogs",
                table: "EventLogs");

            migrationBuilder.RenameTable(
                name: "EventLogs",
                newName: "EventLog");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventLog",
                table: "EventLog",
                column: "Id");
        }
    }
}
