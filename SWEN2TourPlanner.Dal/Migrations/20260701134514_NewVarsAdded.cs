using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWEN2TourPlanner.Dal.Migrations
{
    /// <inheritdoc />
    public partial class NewVarsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ChildFriendliness",
                table: "Tours",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Popularity",
                table: "Tours",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildFriendliness",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Tours");
        }
    }
}
