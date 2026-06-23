using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWEN2TourPlanner.Dal.Migrations
{
    /// <inheritdoc />
    public partial class MakeTourNameNonUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_Name",
                table: "Tours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tours_Name",
                table: "Tours",
                column: "Name",
                unique: true);
        }
    }
}
