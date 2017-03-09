using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FamilyAlbum.Data;

namespace FamilyAlbum.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170309215251_ChangeAlbumName")]
    partial class ChangeAlbumName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FamilyAlbum.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("UserId");

                    b.Property<string>("Zip");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("FamilyAlbum.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("AvatarImgImageId");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("FamilyId");

                    b.Property<string>("FirstName");

                    b.Property<string>("Home");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Mobile");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Work");

                    b.HasKey("Id");

                    b.HasIndex("AvatarImgImageId");

                    b.HasIndex("FamilyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FamilyAlbum.Models.ApplicationUserMessage", b =>
                {
                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("MessageId");

                    b.HasKey("ApplicationUserId", "MessageId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("MessageId");

                    b.ToTable("ApplicationUserMessage");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Family", b =>
                {
                    b.Property<int>("FamilyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Motto");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhotoURL");

                    b.HasKey("FamilyId");

                    b.ToTable("Family");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<DateTime>("Date");

                    b.Property<string>("FilePath");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("PhotoAlbumId");

                    b.Property<byte[]>("UploadTime")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("ImageId");

                    b.HasIndex("PhotoAlbumId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Message", b =>
                {
                    b.Property<string>("MessageId");

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<byte[]>("PostTime")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<bool>("Read");

                    b.Property<string>("SenderId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("MessageId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("FamilyAlbum.Models.PhotoAlbum", b =>
                {
                    b.Property<int>("PhotoAlbumId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DateCreate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("PhotoAlbumId");

                    b.HasIndex("UserId");

                    b.ToTable("PhotoAlbum");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<byte[]>("TStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Reply", b =>
                {
                    b.Property<int>("ReplyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int?>("PostId");

                    b.Property<string>("UserId");

                    b.HasKey("ReplyId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Address", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FamilyAlbum.Models.ApplicationUser", b =>
                {
                    b.HasOne("FamilyAlbum.Models.Image", "AvatarImg")
                        .WithMany()
                        .HasForeignKey("AvatarImgImageId");

                    b.HasOne("FamilyAlbum.Models.Family", "Family")
                        .WithMany("Members")
                        .HasForeignKey("FamilyId");
                });

            modelBuilder.Entity("FamilyAlbum.Models.ApplicationUserMessage", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser", "User")
                        .WithMany("IncomingMessages")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FamilyAlbum.Models.Message", "Message")
                        .WithMany("Recipients")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FamilyAlbum.Models.Image", b =>
                {
                    b.HasOne("FamilyAlbum.Models.PhotoAlbum", "PhotoAlbum")
                        .WithMany("Images")
                        .HasForeignKey("PhotoAlbumId");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Message", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser", "Sender")
                        .WithMany("OutgoingMessages")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("FamilyAlbum.Models.PhotoAlbum", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser", "User")
                        .WithMany("PhotoAlbums")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Post", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FamilyAlbum.Models.Reply", b =>
                {
                    b.HasOne("FamilyAlbum.Models.Post", "Post")
                        .WithMany("Replies")
                        .HasForeignKey("PostId");

                    b.HasOne("FamilyAlbum.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FamilyAlbum.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FamilyAlbum.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
