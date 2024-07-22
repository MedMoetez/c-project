using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppelsDOffresApp.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureAppelDOffreRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppelsDOffres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GestionnaireId = table.Column<int>(type: "int", nullable: false),
                    DescriptionOffre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: true),
                    UtilisateurId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppelsDOffres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppelsDOffres_Utilisateurs_GestionnaireId",
                        column: x => x.GestionnaireId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppelsDOffres_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppelsDOffres_Utilisateurs_UtilisateurId1",
                        column: x => x.UtilisateurId1,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rappels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
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
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Contenu = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AppelDOffreId = table.Column<int>(type: "int", nullable: false),
                    AppelOffreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_AppelsDOffres_AppelOffreId",
                        column: x => x.AppelOffreId,
                        principalTable: "AppelsDOffres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppelDOffreCollaborateurs_UtilisateurId",
                table: "AppelDOffreCollaborateurs",
                column: "UtilisateurId");

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
                name: "IX_Documents_AppelOffreId",
                table: "Documents",
                column: "AppelOffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Rappels_UtilisateurId",
                table: "Rappels",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppelDOffreCollaborateurs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Rappels");

            migrationBuilder.DropTable(
                name: "AppelsDOffres");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
