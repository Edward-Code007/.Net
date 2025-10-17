using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tests.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UserId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_UserId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Role");

            migrationBuilder.CreateTable(
                name: "RoleModelUserModel",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModelUserModel", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleModelUserModel_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleModelUserModel_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleModelUserModel_UserId",
                table: "RoleModelUserModel",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleModelUserModel");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserId",
                table: "Role",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_UserId",
                table: "Role",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
