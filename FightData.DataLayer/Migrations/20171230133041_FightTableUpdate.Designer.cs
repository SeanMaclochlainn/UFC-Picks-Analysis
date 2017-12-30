﻿// <auto-generated />
using FightData.DataLayer;
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
    [Migration("20171230133041_FightTableUpdate")]
    partial class FightTableUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FightData.Models.AltName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("AltName");
                });

            modelBuilder.Entity("FightData.Models.Analyst", b =>
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

            modelBuilder.Entity("FightData.Models.CardType", b =>
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

            modelBuilder.Entity("FightData.Models.Event", b =>
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

            modelBuilder.Entity("FightData.Models.Fight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardTypeId");

                    b.Property<int?>("EventId");

                    b.Property<int?>("LoserId");

                    b.Property<int?>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("CardTypeId");

                    b.HasIndex("EventId");

                    b.HasIndex("LoserId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Fight");
                });

            modelBuilder.Entity("FightData.Models.Fighter", b =>
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

            modelBuilder.Entity("FightData.Models.FighterAltName", b =>
                {
                    b.Property<int>("AltNameId");

                    b.Property<int>("FighterId");

                    b.HasKey("AltNameId", "FighterId");

                    b.HasIndex("FighterId");

                    b.ToTable("FighterAltName");
                });

            modelBuilder.Entity("FightData.Models.Pick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnalystId");

                    b.Property<bool?>("Correct");

                    b.Property<int?>("FightId");

                    b.Property<int>("FighterPickId");

                    b.Property<string>("Pick1")
                        .IsRequired()
                        .HasColumnName("Pick")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("AnalystId");

                    b.HasIndex("FightId");

                    b.ToTable("Pick");
                });

            modelBuilder.Entity("FightData.Models.Webpage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data")
                        .IsUnicode(false);

                    b.Property<int?>("EventId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("URL")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<int?>("WebsiteId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("WebsiteId");

                    b.ToTable("Webpage");
                });

            modelBuilder.Entity("FightData.Models.Website", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Website");
                });

            modelBuilder.Entity("FightData.Models.Fight", b =>
                {
                    b.HasOne("FightData.Models.CardType", "CardType")
                        .WithMany("Fights")
                        .HasForeignKey("CardTypeId");

                    b.HasOne("FightData.Models.Event", "Event")
                        .WithMany("Fights")
                        .HasForeignKey("EventId");

                    b.HasOne("FightData.Models.Fighter", "Loser")
                        .WithMany("Losses")
                        .HasForeignKey("LoserId");

                    b.HasOne("FightData.Models.Fighter", "Winner")
                        .WithMany("Wins")
                        .HasForeignKey("WinnerId");
                });

            modelBuilder.Entity("FightData.Models.FighterAltName", b =>
                {
                    b.HasOne("FightData.Models.AltName", "AltName")
                        .WithMany("Fighters")
                        .HasForeignKey("AltNameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FightData.Models.Fighter", "Fighter")
                        .WithMany("AltNames")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FightData.Models.Pick", b =>
                {
                    b.HasOne("FightData.Models.Analyst", "Analyst")
                        .WithMany("Picks")
                        .HasForeignKey("AnalystId");

                    b.HasOne("FightData.Models.Fight", "Fight")
                        .WithMany("Picks")
                        .HasForeignKey("FightId");
                });

            modelBuilder.Entity("FightData.Models.Webpage", b =>
                {
                    b.HasOne("FightData.Models.Event", "Event")
                        .WithMany("Webpages")
                        .HasForeignKey("EventId");

                    b.HasOne("FightData.Models.Website", "Website")
                        .WithMany("Webpages")
                        .HasForeignKey("WebsiteId");
                });
#pragma warning restore 612, 618
        }
    }
}
