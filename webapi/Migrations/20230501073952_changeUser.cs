using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class changeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RolesId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "User",
                newName: "RolesID");

            migrationBuilder.RenameIndex(
                name: "IX_User_RolesId",
                table: "User",
                newName: "IX_User_RolesID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RolesID",
                table: "User",
                column: "RolesID",
                principalTable: "Roles",
                principalColumn: "RolesID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RolesID",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RolesID",
                table: "User",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RolesID",
                table: "User",
                newName: "IX_User_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RolesId",
                table: "User",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "RolesID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
