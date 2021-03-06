// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trails.Data;

#nullable disable

namespace Trails.Data.Migrations
{
    [DbContext(typeof(TrailsDbContext))]
    partial class TrailsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Beacon", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("Imei")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("KeyHash")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SimCardNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Beacons");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.BeaconData", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<double>("Altitude")
                        .HasColumnType("float");

                    b.Property<string>("BeaconId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("BeaconImei")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BeaconId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("BeaconData");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModifiedByCreator")
                        .HasColumnType("bit");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RouteId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ImageId");

                    b.HasIndex("RouteId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("DataBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Title")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Participant", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("BeaconId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BeaconId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Route", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("FinishLocationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<double>("MaximumAltitude")
                        .HasColumnType("float");

                    b.Property<double>("MinimumAltitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StartLocationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("EventId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.RoutePoint", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<double>("Altitude")
                        .HasColumnType("float");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("RouteId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RoutePoints");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Trails.Data.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Trails.Data.DomainModels.BeaconData", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.Beacon", "Beacon")
                        .WithMany("BeaconData")
                        .HasForeignKey("BeaconId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Trails.Data.DomainModels.Participant", "Participant")
                        .WithMany("BeaconData")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Beacon");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Event", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Trails.Data.DomainModels.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Trails.Data.DomainModels.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Creator");

                    b.Navigation("Image");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Image", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Participant", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.Beacon", "Beacon")
                        .WithMany()
                        .HasForeignKey("BeaconId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Trails.Data.DomainModels.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Trails.Data.DomainModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Beacon");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Route", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Trails.Data.DomainModels.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Creator");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.RoutePoint", b =>
                {
                    b.HasOne("Trails.Data.DomainModels.Route", "Route")
                        .WithMany("RoutePoints")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Beacon", b =>
                {
                    b.Navigation("BeaconData");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Event", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Participant", b =>
                {
                    b.Navigation("BeaconData");
                });

            modelBuilder.Entity("Trails.Data.DomainModels.Route", b =>
                {
                    b.Navigation("RoutePoints");
                });
#pragma warning restore 612, 618
        }
    }
}
