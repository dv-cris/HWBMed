using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HWBMed.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelEmployeeArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeeAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmployee = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdArea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeAreas_Areas_IdArea",
                        column: x => x.IdArea,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeAreas_AspNetUsers_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeeAreas_IdArea",
                table: "employeeAreas",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_employeeAreas_IdEmployee",
                table: "employeeAreas",
                column: "IdEmployee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeAreas");
        }
    }
}
