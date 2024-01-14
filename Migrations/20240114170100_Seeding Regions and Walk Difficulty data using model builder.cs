using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRegionsandWalkDifficultydatausingmodelbuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7d65def3-951c-4096-b5c4-53cfe22781ac"), "Medium" },
                    { new Guid("9fd5c601-84ea-4524-8ab3-a3824ce75d06"), "Hard" },
                    { new Guid("b37d3815-9d7f-44ae-9f05-1baf797a4827"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("05a918ea-c136-4af3-96d9-642da12cef54"), "OTG", "Otago", "some_image_url.jpeg" },
                    { new Guid("6f771621-ad5d-4da5-8df4-4c35f1b25c8f"), "AKL", "Auckland", "some_image_url.jpeg" },
                    { new Guid("885b4079-8dcf-4ad3-9ea2-931123debac6"), "WLN", "Wellington", "some_image_url.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7d65def3-951c-4096-b5c4-53cfe22781ac"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9fd5c601-84ea-4524-8ab3-a3824ce75d06"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b37d3815-9d7f-44ae-9f05-1baf797a4827"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("05a918ea-c136-4af3-96d9-642da12cef54"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6f771621-ad5d-4da5-8df4-4c35f1b25c8f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("885b4079-8dcf-4ad3-9ea2-931123debac6"));
        }
    }
}
