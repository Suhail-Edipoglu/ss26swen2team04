using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWEN2TourPlanner.Dal.Migrations
{
    /// <inheritdoc />
    public partial class MakeTourNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tours_Name",
                table: "Tours",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_Name",
                table: "Tours");
        }
    }
}
