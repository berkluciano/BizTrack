using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniProject1.Migrations
{
    /// <inheritdoc />
    public partial class InitPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "TodoItems",
                newName: "PriorityId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TodoItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_PriorityId",
                table: "TodoItems",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_UserId",
                table: "TodoItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Priorities_PriorityId",
                table: "TodoItems",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Users_UserId",
                table: "TodoItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Priorities_PriorityId",
                table: "TodoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Users_UserId",
                table: "TodoItems");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_PriorityId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_UserId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "TodoItems",
                newName: "Priority");
        }
    }
}
