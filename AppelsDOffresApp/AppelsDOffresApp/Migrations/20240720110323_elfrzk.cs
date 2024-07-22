﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppelsDOffresApp.Migrations
{
    /// <inheritdoc />
    public partial class elfrzk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AppelsDOffres_AppelDOffreId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AppelDOffreId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "AppelDOffreId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Contenu",
                table: "Documents");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AppelDOffreId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Contenu",
                table: "Documents",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

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
    }
}
