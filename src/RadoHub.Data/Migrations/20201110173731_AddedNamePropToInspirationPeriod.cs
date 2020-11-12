using Microsoft.EntityFrameworkCore.Migrations;

namespace RadoHub.WebApp.Data.Migrations
{
    public partial class AddedNamePropToInspirationPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InspirationPeriods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InspirationPeriods");
        }
    }
}
