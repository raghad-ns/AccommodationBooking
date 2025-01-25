using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccommodationBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class identifyCityOptionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dca3a58-14fb-41e3-9805-5c9a7456bb64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cce2987c-26df-4495-b782-c2d8424ec105");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2351511e-5ff1-4473-8a10-297c52ac267b", null, "Admin", "ADMIN" },
                    { "acd9bd99-c4b0-4404-a4c0-18580d67e1b2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2351511e-5ff1-4473-8a10-297c52ac267b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acd9bd99-c4b0-4404-a4c0-18580d67e1b2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1dca3a58-14fb-41e3-9805-5c9a7456bb64", null, "Admin", "ADMIN" },
                    { "cce2987c-26df-4495-b782-c2d8424ec105", null, "User", "USER" }
                });
        }
    }
}
