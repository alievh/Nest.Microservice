using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest.Services.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "ordering",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "ordering",
                table: "OrderItems");
        }
    }
}
