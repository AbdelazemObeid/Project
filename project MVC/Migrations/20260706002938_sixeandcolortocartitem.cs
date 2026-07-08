using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class sixeandcolortocartitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "cart_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "size",
                table: "cart_items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "size",
                table: "cart_items");
        }
    }
}
