using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FightData.Models.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FightData.DataLayer
{
    public class FightPicksContext : DbContext
    {
        public FightPicksContext() { }

        public FightPicksContext(DbContextOptions<FightPicksContext> options) : base(options) { }

        public DbSet<FighterAltName> FighterAltNames { get; set; }
        public DbSet<AnalystAltName> AnalystAltNames { get; set; }
        public DbSet<Analyst> Analysts { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<Pick> Picks { get; set; }
        public DbSet<Webpage> Webpages { get; set; }
        public DbSet<Website> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-BM2NFSS0;Database=FightPicks.Dev;Integrated Security=SSPI;MultipleActiveResultSets=true");
            }
        }

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

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>().Ignore(e=>e.CancelledFighterNames);
            modelBuilder.Entity<Event>().Ignore(e => e.FightersWithMatchingLastNames);

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
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.Property(e => e.DomainName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Webpage>()
                .HasOne(wp => wp.Website)
                .WithMany(ws => ws.Webpages)
                .IsRequired();

            modelBuilder.Entity<Webpage>()
                .HasOne(wp => wp.Event)
                .WithMany(e => e.Webpages)
                .IsRequired();

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Winner)
                .WithMany(ft => ft.Wins);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Loser)
                .WithMany(ft => ft.Losses);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Fights)
                .IsRequired();

            modelBuilder.Entity<Pick>()
                .HasOne(p => p.Fight)
                .WithMany(f => f.Picks)
                .IsRequired();

            modelBuilder.Entity<Pick>()
                .HasOne(p => p.FighterPick)
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

        }
    }
}
