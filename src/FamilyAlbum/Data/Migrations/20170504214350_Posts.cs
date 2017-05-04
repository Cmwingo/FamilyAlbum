using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyAlbum.Data.Migrations
{
    public partial class Posts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostFamilyFamilyId",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostTime",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostFamilyFamilyId",
                table: "Post",
                column: "PostFamilyFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Family_PostFamilyFamilyId",
                table: "Post",
                column: "PostFamilyFamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Family_PostFamilyFamilyId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_PostFamilyFamilyId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "PostFamilyFamilyId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "PostTime",
                table: "Post");
        }
    }
}
