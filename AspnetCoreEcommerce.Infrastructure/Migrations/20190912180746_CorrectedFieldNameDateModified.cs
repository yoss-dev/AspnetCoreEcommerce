using Microsoft.EntityFrameworkCore.Migrations;

namespace AspnetCoreEcommerce.Infrastructure.Migrations
{
    public partial class CorrectedFieldNameDateModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatedModified",
                table: "Product",
                newName: "DateModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Product",
                newName: "DatedModified");
        }
    }
}
