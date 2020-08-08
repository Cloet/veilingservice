﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using veilingservice.Data;

namespace veilingservice.Migrations
{
    [DbContext(typeof(VeilingContext))]
    partial class AuctionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("veilingservice.Model.ApiKey", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Owner")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("ApiKey");
                });

            modelBuilder.Entity("veilingservice.Model.Auction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Overview")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Auction");
                });

            modelBuilder.Entity("veilingservice.Model.AuctionImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AuctionID")
                        .HasColumnType("integer");

                    b.Property<string>("ImageLocation")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AuctionID");

                    b.ToTable("AuctionImage");
                });

            modelBuilder.Entity("veilingservice.Model.Lot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AmountOfBids")
                        .HasColumnType("integer");

                    b.Property<int>("AuctionID")
                        .HasColumnType("integer");

                    b.Property<double>("Bid")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentBid")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<double>("OpeningsBid")
                        .HasColumnType("double precision");

                    b.Property<string>("Overview")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(60)")
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.HasIndex("AuctionID");

                    b.ToTable("Lot");
                });

            modelBuilder.Entity("veilingservice.Model.LotImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ImageLocation")
                        .HasColumnType("text");

                    b.Property<int>("LotID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("LotImage");
                });

            modelBuilder.Entity("veilingservice.Model.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ApiKeyID")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ApiKeyID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("veilingservice.Model.AuctionImage", b =>
                {
                    b.HasOne("veilingservice.Model.Auction", null)
                        .WithMany("Images")
                        .HasForeignKey("AuctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("veilingservice.Model.Lot", b =>
                {
                    b.HasOne("veilingservice.Model.Auction", null)
                        .WithMany("Lots")
                        .HasForeignKey("AuctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("veilingservice.Model.Role", b =>
                {
                    b.HasOne("veilingservice.Model.ApiKey", null)
                        .WithMany("Roles")
                        .HasForeignKey("ApiKeyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
