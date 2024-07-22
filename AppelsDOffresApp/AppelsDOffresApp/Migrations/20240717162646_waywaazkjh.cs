using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppelsDOffresApp.Migrations
{
    /// <inheritdoc />
    public partial class waywaazkjh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppelsDOffres_Utilisateurs_GestionnaireId",
                table: "AppelsDOffres");

            migrationBuilder.DropForeignKey(
                name: "FK_AppelsDOffres_Utilisateurs_UtilisateurId",
                table: "AppelsDOffres");

            migrationBuilder.DropForeignKey(
                name: "FK_AppelsDOffres_Utilisateurs_UtilisateurId1",
                table: "AppelsDOffres");

            migrationBuilder.DropTable(
                name: "AppelDOffreCollaborateurs");

            migrationBuilder.DropTable(
                name: "Rappels");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_AppelsDOffres_GestionnaireId",
                table: "AppelsDOffres");

            migrationBuilder.DropIndex(
                name: "IX_AppelsDOffres_UtilisateurId",
                table: "AppelsDOffres");

            migrationBuilder.DropIndex(
                name: "IX_AppelsDOffres_UtilisateurId1",
                table: "AppelsDOffres");

            migrationBuilder.DropColumn(
                name: "DescriptionOffre",
                table: "AppelsDOffres");

            migrationBuilder.DropColumn(
                name: "GestionnaireId",
                table: "AppelsDOffres");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "AppelsDOffres");

            migrationBuilder.DropColumn(
                name: "UtilisateurId1",
                table: "AppelsDOffres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionOffre",
                table: "AppelsDOffres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GestionnaireId",
                table: "AppelsDOffres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "AppelsDOffres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId1",
                table: "AppelsDOffres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppelDOffreCollaborateurs",
                columns: table => new
                {
                    AppelDOffreId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppelDOffreCollaborateurs", x => new { x.AppelDOffreId, x.UtilisateurId });
                    table.ForeignKey(
                        name: "FK_AppelDOffreCollaborateurs_AppelsDOffres_AppelDOffreId",
                        column: x => x.AppelDOffreId,
                        principalTable: "AppelsDOffres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppelDOffreCollaborateurs_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rappels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rappels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rappels_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppelsDOffres_GestionnaireId",
                table: "AppelsDOffres",
                column: "GestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_AppelsDOffres_UtilisateurId",
                table: "AppelsDOffres",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_AppelsDOffres_UtilisateurId1",
                table: "AppelsDOffres",
                column: "UtilisateurId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppelDOffreCollaborateurs_UtilisateurId",
                table: "AppelDOffreCollaborateurs",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Rappels_UtilisateurId",
                table: "Rappels",
                column: "UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppelsDOffres_Utilisateurs_GestionnaireId",
                table: "AppelsDOffres",
                column: "GestionnaireId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppelsDOffres_Utilisateurs_UtilisateurId",
                table: "AppelsDOffres",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppelsDOffres_Utilisateurs_UtilisateurId1",
                table: "AppelsDOffres",
                column: "UtilisateurId1",
                principalTable: "Utilisateurs",
                principalColumn: "Id");
        }
    }
}
