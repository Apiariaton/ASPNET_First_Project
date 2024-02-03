using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileSizeInButes",
                table: "Images",
                newName: "FileSizeInBytes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileSizeInBytes",
                table: "Images",
                newName: "FileSizeInButes");
        }
    }
}
