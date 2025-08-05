using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace So2Baladna.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_order_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyerEmai",
                table: "Orders",
                newName: "BuyerEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyerEmail",
                table: "Orders",
                newName: "BuyerEmai");
        }
    }
}
