using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameFellowship.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGameFollowers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Followers",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Followers",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
