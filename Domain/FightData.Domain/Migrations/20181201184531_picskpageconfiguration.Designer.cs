﻿// <auto-generated />
using System;
using FightData.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FightData.Domain.Migrations
{
    [DbContext(typeof(FightPicksContext))]
    [Migration("20181201184531_picskpageconfiguration")]
    partial class picskpageconfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-t000")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FightData.Domain.Entities.Analyst", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int?>("WebsiteId");

                    b.HasKey("Id");

                    b.HasIndex("WebsiteId");

                    b.ToTable("Analyst");
                });

            modelBuilder.Entity("FightData.Domain.Entities.AnalystAltName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnalystId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AnalystId");

                    b.ToTable("AnalystAltName");
                });

            modelBuilder.Entity("FightData.Domain.Entities.CardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(180)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("CardType");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Exhibition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Exhibition");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Fight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardTypeId");

                    b.Property<int?>("ExhibitionId")
                        .IsRequired();

                    b.Property<int?>("LoserId");

                    b.Property<int?>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("CardTypeId");

                    b.HasIndex("ExhibitionId");

                    b.HasIndex("LoserId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Fight");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Fighter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Fighter");
                });

            modelBuilder.Entity("FightData.Domain.Entities.FighterAltName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FighterId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("FighterId");

                    b.ToTable("FighterAltName");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Odd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FightId")
                        .IsRequired();

                    b.Property<int?>("FighterId")
                        .IsRequired();

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("FightId");

                    b.HasIndex("FighterId");

                    b.ToTable("Odd");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Pick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnalystId")
                        .IsRequired();

                    b.Property<int?>("FightId")
                        .IsRequired();

                    b.Property<int?>("FighterId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AnalystId");

                    b.HasIndex("FightId");

                    b.HasIndex("FighterId");

                    b.ToTable("Pick");
                });

            modelBuilder.Entity("FightData.Domain.Entities.PicksPageConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnalystXpath")
                        .IsRequired();

                    b.Property<string>("FighterXpath")
                        .IsRequired();

                    b.Property<int>("PicksPageRowType");

                    b.Property<int>("WebsiteId");

                    b.HasKey("Id");

                    b.HasIndex("WebsiteId")
                        .IsUnique();

                    b.ToTable("PicksPageConfiguration");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Webpage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .IsUnicode(false);

                    b.Property<int?>("ExhibitionId")
                        .IsRequired();

                    b.Property<bool>("Parsed");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("URL")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<int?>("WebsiteId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ExhibitionId");

                    b.HasIndex("WebsiteId");

                    b.ToTable("Webpage");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Website", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("WebsiteName");

                    b.Property<int>("WebsiteType");

                    b.HasKey("Id");

                    b.ToTable("Website");
                });

            modelBuilder.Entity("FightData.Domain.Entities.Analyst", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Website", "Website")
                        .WithMany()
                        .HasForeignKey("WebsiteId");
                });

            modelBuilder.Entity("FightData.Domain.Entities.AnalystAltName", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Analyst", "Analyst")
                        .WithMany("AltNames")
                        .HasForeignKey("AnalystId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Domain.Entities.Fight", b =>
                {
                    b.HasOne("FightData.Domain.Entities.CardType", "CardType")
                        .WithMany("Fights")
                        .HasForeignKey("CardTypeId");

                    b.HasOne("FightData.Domain.Entities.Exhibition", "Exhibition")
                        .WithMany("Fights")
                        .HasForeignKey("ExhibitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Domain.Entities.Fighter", "Loser")
                        .WithMany("Losses")
                        .HasForeignKey("LoserId");

                    b.HasOne("FightData.Domain.Entities.Fighter", "Winner")
                        .WithMany("Wins")
                        .HasForeignKey("WinnerId");
                });

            modelBuilder.Entity("FightData.Domain.Entities.FighterAltName", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Fighter", "Fighter")
                        .WithMany("FighterAltNames")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Domain.Entities.Odd", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Fight", "Fight")
                        .WithMany("Odds")
                        .HasForeignKey("FightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Domain.Entities.Fighter", "Fighter")
                        .WithMany("Odds")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Domain.Entities.Pick", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Analyst", "Analyst")
                        .WithMany("Picks")
                        .HasForeignKey("AnalystId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Domain.Entities.Fight", "Fight")
                        .WithMany("Picks")
                        .HasForeignKey("FightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Domain.Entities.Fighter", "Fighter")
                        .WithMany("Picks")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Domain.Entities.PicksPageConfiguration", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Website", "Website")
                        .WithOne("PicksPageConfiguration")
                        .HasForeignKey("FightData.Domain.Entities.PicksPageConfiguration", "WebsiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Domain.Entities.Webpage", b =>
                {
                    b.HasOne("FightData.Domain.Entities.Exhibition", "Exhibition")
                        .WithMany("Webpages")
                        .HasForeignKey("ExhibitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Domain.Entities.Website", "Website")
                        .WithMany("Webpages")
                        .HasForeignKey("WebsiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}