using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentStoreManagement.Infrastructure.Migrations.PostgresApplication
{
    /// <inheritdoc />
    public partial class InitialPostgresDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PublisherName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ReleaseQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
