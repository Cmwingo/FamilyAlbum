using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyAlbum.Data.Migrations
{
    public partial class FamilyAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "PhotoAlbum");

            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "PhotoAlbum",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoAlbum_FamilyId",
                table: "PhotoAlbum",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoAlbum_Family_FamilyId",
                table: "PhotoAlbum",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoAlbum_Family_FamilyId",
                table: "PhotoAlbum");

            migrationBuilder.DropIndex(
                name: "IX_PhotoAlbum_FamilyId",
                table: "PhotoAlbum");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "PhotoAlbum");

            migrationBuilder.AddColumn<byte[]>(
                name: "DateCreate",
                table: "PhotoAlbum",
                rowVersion: true,
                nullable: true);
        }
    }
}
