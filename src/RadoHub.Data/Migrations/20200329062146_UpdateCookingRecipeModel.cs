using Microsoft.EntityFrameworkCore.Migrations;

namespace RadoHub.WebApp.Data.Migrations
{
    public partial class UpdateCookingRecipeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditorsUsernames",
                table: "CookingRecipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hashtags",
                table: "CookingRecipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagesFileNames",
                table: "CookingRecipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Products",
                table: "CookingRecipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorsUsernames",
                table: "CookingRecipes");

            migrationBuilder.DropColumn(
                name: "Hashtags",
                table: "CookingRecipes");

            migrationBuilder.DropColumn(
                name: "ImagesFileNames",
                table: "CookingRecipes");

            migrationBuilder.DropColumn(
                name: "Products",
                table: "CookingRecipes");
        }
    }
}
