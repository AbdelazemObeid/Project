using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddSizeAndColorToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "selected_color",
                table: "cart_items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "selected_size",
                table: "cart_items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "selected_color",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "selected_size",
                table: "cart_items");
        }
    }
}
