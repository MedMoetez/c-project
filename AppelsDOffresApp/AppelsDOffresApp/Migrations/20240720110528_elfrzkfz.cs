using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppelsDOffresApp.Migrations
{
    /// <inheritdoc />
    public partial class elfrzkfz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AppelsDOffres_AppelOffreId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AppelOffreId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "AppelOffreId",
                table: "Documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppelOffreId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AppelOffreId",
                table: "Documents",
                column: "AppelOffreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AppelsDOffres_AppelOffreId",
                table: "Documents",
                column: "AppelOffreId",
                principalTable: "AppelsDOffres",
                principalColumn: "Id");
        }
    }
}
