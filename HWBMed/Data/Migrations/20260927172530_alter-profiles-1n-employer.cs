using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HWBMed.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterprofiles1nemployer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdProfile",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_EmployeeId",
                table: "Profiles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdProfile",
                table: "AspNetUsers",
                column: "IdProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_EmployeeId",
                table: "Profiles",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_EmployeeId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_EmployeeId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Profiles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdProfile",
                table: "AspNetUsers",
                column: "IdProfile",
                unique: true,
                filter: "[IdProfile] IS NOT NULL");
        }
    }
}
