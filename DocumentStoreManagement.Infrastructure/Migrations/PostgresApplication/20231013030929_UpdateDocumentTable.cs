using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentStoreManagement.Infrastructure.Migrations.PostgresApplication
{
    /// <inheritdoc />
    public partial class UpdateDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Documents",
                type: "numeric(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Documents");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetails",
                type: "numeric(18,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
