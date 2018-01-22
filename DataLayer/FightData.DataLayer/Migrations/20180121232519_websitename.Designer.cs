﻿// <auto-generated />
using FightData.DataLayer;
using FightData.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FightData.DataLayer.Migrations
{
    [DbContext(typeof(FightPicksContext))]
    [Migration("20180121232519_websitename")]
    partial class websitename
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FightData.Models.DataModels.Analyst", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Analyst");
                });

            modelBuilder.Entity("FightData.Models.DataModels.AnalystAltName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnalystId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AnalystId");

                    b.ToTable("AnalystAltName");
                });

            modelBuilder.Entity("FightData.Models.DataModels.CardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(180)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("CardType");
                });

            modelBuilder.Entity("FightData.Models.DataModels.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("FightData.Models.DataModels.Fight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardTypeId");

                    b.Property<int?>("EventId")
                        .IsRequired();

                    b.Property<int?>("LoserId");

                    b.Property<int?>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("CardTypeId");

                    b.HasIndex("EventId");

                    b.HasIndex("LoserId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Fight");
                });

            modelBuilder.Entity("FightData.Models.DataModels.Fighter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("FightData.Models.DataModels.FighterAltName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("FightData.Models.DataModels.Pick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnalystId")
                        .IsRequired();

                    b.Property<int?>("FightId")
                        .IsRequired();

                    b.Property<int?>("FighterPickId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AnalystId");

                    b.HasIndex("FightId");

                    b.HasIndex("FighterPickId");

                    b.ToTable("Pick");
                });

            modelBuilder.Entity("FightData.Models.DataModels.Webpage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data")
                        .IsUnicode(false);

                    b.Property<int?>("EventId")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("URL")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<int?>("WebsiteId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("WebsiteId");

                    b.ToTable("Webpage");
                });

            modelBuilder.Entity("FightData.Models.DataModels.Website", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("WebsiteName");

                    b.HasKey("Id");

                    b.ToTable("Website");
                });

            modelBuilder.Entity("FightData.Models.DataModels.AnalystAltName", b =>
                {
                    b.HasOne("FightData.Models.DataModels.Analyst", "Analyst")
                        .WithMany("AltNames")
                        .HasForeignKey("AnalystId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Models.DataModels.Fight", b =>
                {
                    b.HasOne("FightData.Models.DataModels.CardType", "CardType")
                        .WithMany("Fights")
                        .HasForeignKey("CardTypeId");

                    b.HasOne("FightData.Models.DataModels.Event", "Event")
                        .WithMany("Fights")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Models.DataModels.Fighter", "Loser")
                        .WithMany("Losses")
                        .HasForeignKey("LoserId");

                    b.HasOne("FightData.Models.DataModels.Fighter", "Winner")
                        .WithMany("Wins")
                        .HasForeignKey("WinnerId");
                });

            modelBuilder.Entity("FightData.Models.DataModels.FighterAltName", b =>
                {
                    b.HasOne("FightData.Models.DataModels.Fighter", "Fighter")
                        .WithMany("AltNames")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Models.DataModels.Pick", b =>
                {
                    b.HasOne("FightData.Models.DataModels.Analyst", "Analyst")
                        .WithMany("Picks")
                        .HasForeignKey("AnalystId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Models.DataModels.Fight", "Fight")
                        .WithMany("Picks")
                        .HasForeignKey("FightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Models.DataModels.Fighter", "FighterPick")
                        .WithMany("Picks")
                        .HasForeignKey("FighterPickId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Models.DataModels.Webpage", b =>
                {
                    b.HasOne("FightData.Models.DataModels.Event", "Event")
                        .WithMany("Webpages")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Models.DataModels.Website", "Website")
                        .WithMany("Webpages")
                        .HasForeignKey("WebsiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
