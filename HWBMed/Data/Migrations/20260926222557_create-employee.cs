using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HWBMed.Data.Migrations
{
    /// <inheritdoc />
    public partial class createemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdProfile",
                table: "AspNetUsers",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "AspNetUsers",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdProfile",
                table: "AspNetUsers",
                column: "IdProfile",
                unique: true,
                filter: "[IdProfile] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdUser",
                table: "AspNetUsers",
                column: "IdUser",
                unique: true,
                filter: "[IdUser] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profiles_IdProfile",
                table: "AspNetUsers",
                column: "IdProfile",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_IdUser",
                table: "AspNetUsers",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profiles_IdProfile",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_IdUser",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdProfile",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "AspNetUsers");
        }
    }
}
