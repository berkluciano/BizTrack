using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniProject1.Migrations
{
    /// <inheritdoc />
    public partial class FixUserIdUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "TodoItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "TodoItems");
        }
    }
}
