using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FamilyAlbum.Data.Migrations
{
    public partial class Messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint("FK_ApplicationUserMessage_Message_MessageId", "ApplicationUserMessage");
            migrationBuilder.DropUniqueConstraint("PK_ApplicationUserMessage", "ApplicationUserMessage");
            migrationBuilder.DropIndex("IX_ApplicationUserMessage_MessageId", "ApplicationUserMessage");
            migrationBuilder.DropUniqueConstraint("PK_Message", "Message");
            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Message",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "ApplicationUserMessage",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "Message",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "ApplicationUserMessage",
                nullable: false);
        }
    }
}
