using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWEN2TourPlanner.Dal.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTourField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Tours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Tours",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
