using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Scbwi.Models;

namespace Scbwi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160329212057_setup")]
    partial class setup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
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
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("Scbwi.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

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

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Scbwi.Models.Comprehensive", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<int>("maxattendees");

                    b.Property<string>("presenters");

                    b.Property<decimal>("price");

                    b.Property<string>("title");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Scbwi.Models.CouponCode", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("numuses");

                    b.Property<string>("text");

                    b.Property<int>("type");

                    b.Property<decimal>("value");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Scbwi.Models.Critique", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Registrationid");

                    b.Property<decimal>("price");

                    b.Property<string>("type");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Scbwi.Models.ImportantDate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<string>("title");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Scbwi.Models.Package", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<decimal>("lateprice");

                    b.Property<int>("max");

                    b.Property<bool>("member");

                    b.Property<decimal>("regularprice");

                    b.Property<string>("title");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Scbwi.Models.Registration", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address1");

                    b.Property<string>("address2");

                    b.Property<string>("city");

                    b.Property<DateTime>("cleared");

                    b.Property<int?>("codeid");

                    b.Property<int?>("comprehensiveid");

                    b.Property<string>("country");

                    b.Property<string>("email");

                    b.Property<string>("first");

                    b.Property<string>("last");

                    b.Property<int?>("packageid");

                    b.Property<DateTime>("paid");

                    b.Property<string>("paypalid");

                    b.Property<string>("phone");

                    b.Property<string>("state");

                    b.Property<DateTime>("submitted");

                    b.Property<decimal>("subtotal");

                    b.Property<decimal>("total");

                    b.Property<int?>("trackid");

                    b.Property<string>("zip");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Scbwi.Models.Track", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<string>("presenters");

                    b.Property<string>("title");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Scbwi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Scbwi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Scbwi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Scbwi.Models.Critique", b =>
                {
                    b.HasOne("Scbwi.Models.Registration")
                        .WithMany()
                        .HasForeignKey("Registrationid");
                });

            modelBuilder.Entity("Scbwi.Models.Registration", b =>
                {
                    b.HasOne("Scbwi.Models.CouponCode")
                        .WithMany()
                        .HasForeignKey("codeid");

                    b.HasOne("Scbwi.Models.Comprehensive")
                        .WithMany()
                        .HasForeignKey("comprehensiveid");

                    b.HasOne("Scbwi.Models.Package")
                        .WithMany()
                        .HasForeignKey("packageid");

                    b.HasOne("Scbwi.Models.Track")
                        .WithMany()
                        .HasForeignKey("trackid");
                });
        }
    }
}
