﻿using FightData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FightData.Domain
{
    public class FightPicksContext : DbContext
    {
        public FightPicksContext(DbContextOptions<FightPicksContext> options) : base(options) { }

        public DbSet<FighterAltName> FighterAltNames { get; set; }
        public DbSet<AnalystAltName> AnalystAltNames { get; set; }
        public DbSet<Analyst> Analysts { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<Pick> Picks { get; set; }
        public DbSet<Webpage> Webpages { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Odd> Odds { get; set; }
        public DbSet<PicksPageConfiguration> PicksPageConfigurations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            modelBuilder.Entity<FighterAltName>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Analyst>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(180)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                .IsRequired(true);
            });

            modelBuilder.Entity<Fighter>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Webpage>(entity =>
            {
                entity.Property(e => e.Data).IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Parsed)
                .IsRequired();
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.Property(e => e.WebsiteName)
                .IsRequired();
            });

            modelBuilder.Entity<Odd>(entity =>
            {
                entity.Property(e => e.Value)
                .IsRequired();
            });

            modelBuilder.Entity<PicksPageConfiguration>(entity =>
            {
                entity.Property(e => e.PicksPageRowType)
                .IsRequired();

                entity.Property(e => e.AnalystXpath)
                .IsRequired();

                entity.Property(e => e.AnalystRegex)
                .IsRequired(false);

                entity.Property(e => e.FighterXpath)
                .IsRequired();

                entity.Property(e => e.FighterRegex)
                .IsRequired(false);

            });

            modelBuilder.Entity<Webpage>()
                .HasOne(wp => wp.Website)
                .WithMany(ws => ws.Webpages)
                .IsRequired();

            modelBuilder.Entity<Webpage>()
                .HasOne(wp => wp.Exhibition)
                .WithMany(e => e.Webpages)
                .IsRequired();

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Winner)
                .WithMany(ft => ft.Wins);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Loser)
                .WithMany(ft => ft.Losses);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Exhibition)
                .WithMany(e => e.Fights)
                .IsRequired();

            modelBuilder.Entity<Pick>()
                .HasOne(p => p.Fight)
                .WithMany(f => f.Picks)
                .IsRequired();

            modelBuilder.Entity<Pick>()
                .HasOne(p => p.Fighter)
                .WithMany(fp => fp.Picks)
                .IsRequired();

            modelBuilder.Entity<Pick>()
                .HasOne(p => p.Analyst)
                .WithMany(a => a.Picks)
                .IsRequired();

            modelBuilder.Entity<FighterAltName>()
                .HasOne(an => an.Fighter)
                .WithMany(f => f.FighterAltNames)
                .IsRequired();

            modelBuilder.Entity<AnalystAltName>()
                .HasOne(a => a.Analyst)
                .WithMany(a => a.AltNames)
                .IsRequired();

            modelBuilder.Entity<Odd>()
                .HasOne(o => o.Fighter)
                .WithMany(f => f.Odds)
                .IsRequired();

            modelBuilder.Entity<Odd>()
                .HasOne(o => o.Fight)
                .WithMany(f => f.Odds)
                .IsRequired();

            modelBuilder.Entity<Website>()
               .HasOne(w => w.PicksPageConfiguration)
               .WithOne(ppc => ppc.Website)
               .HasForeignKey<PicksPageConfiguration>(e => e.WebsiteId);

        }
    }
}
