using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppelsDOffresApp.Migrations
{
    /// <inheritdoc />
    public partial class Doc : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AppelDOffreId",
                table: "Documents",
                column: "AppelDOffreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AppelsDOffres_AppelDOffreId",
                table: "Documents",
                column: "AppelDOffreId",
                principalTable: "AppelsDOffres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AppelsDOffres_AppelDOffreId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AppelDOffreId",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "AppelOffreId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AppelOffreId",
                table: "Documents",
                column: "AppelOffreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AppelsDOffres_AppelOffreId",
                table: "Documents",
                column: "AppelOffreId",
                principalTable: "AppelsDOffres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
