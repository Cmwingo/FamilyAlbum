using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyAlbum.Data.Migrations
{
    public partial class ChangeAlbumName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_PhotoAlbum_AlbumPhotoAlbumId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_AlbumPhotoAlbumId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "AlbumPhotoAlbumId",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "PhotoAlbumId",
                table: "Image",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_PhotoAlbumId",
                table: "Image",
                column: "PhotoAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_PhotoAlbum_PhotoAlbumId",
                table: "Image",
                column: "PhotoAlbumId",
                principalTable: "PhotoAlbum",
                principalColumn: "PhotoAlbumId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_PhotoAlbum_PhotoAlbumId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_PhotoAlbumId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PhotoAlbumId",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "AlbumPhotoAlbumId",
                table: "Image",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_AlbumPhotoAlbumId",
                table: "Image",
                column: "AlbumPhotoAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_PhotoAlbum_AlbumPhotoAlbumId",
                table: "Image",
                column: "AlbumPhotoAlbumId",
                principalTable: "PhotoAlbum",
                principalColumn: "PhotoAlbumId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
