using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace So2Baladna.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "DeliveryTime", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "around 1 week", "the fast Delivery in the world", "Tyaara", 150.54m },
                    { 2, "around 2 week", "the fast Delivery in the world", "DHL", 150m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
