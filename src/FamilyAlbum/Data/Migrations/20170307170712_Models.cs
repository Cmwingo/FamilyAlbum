using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FamilyAlbum.Data.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    FamilyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Motto = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PhotoURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.FamilyId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoAlbum",
                columns: table => new
                {
                    PhotoAlbumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreate = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoAlbum", x => x.PhotoAlbumId);
                    table.ForeignKey(
                        name: "FK_PhotoAlbum_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    TStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlbumPhotoAlbumId = table.Column<int>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    UploadTime = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_PhotoAlbum_AlbumPhotoAlbumId",
                        column: x => x.AlbumPhotoAlbumId,
                        principalTable: "PhotoAlbum",
                        principalColumn: "PhotoAlbumId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "AvatarImgImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Home",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Work",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarImgImageId",
                table: "AspNetUsers",
                column: "AvatarImgImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_AlbumPhotoAlbumId",
                table: "Image",
                column: "AlbumPhotoAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoAlbum_UserId",
                table: "PhotoAlbum",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Image_AvatarImgImageId",
                table: "AspNetUsers",
                column: "AvatarImgImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Family_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Image_AvatarImgImageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Family_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarImgImageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarImgImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Home",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Work",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "PhotoAlbum");
        }
    }
}
