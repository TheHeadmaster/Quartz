using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PersistentEntity;
using ReactiveUI;

namespace Quartz.Core.ObjectModel
{
    public class QuartzContext : DatabaseContext
    {
        public DbSet<CollisionMap> CollisionMaps { get; set; } = null!;

        public DbSet<DirectionMap> DirectionMaps { get; set; } = null!;

        public DbSet<ElementMatchup> ElementMatchups { get; set; } = null!;

        public DbSet<Element> Elements { get; set; } = null!;

        public DbSet<MapLayer> MapLayers { get; set; } = null!;

        public DbSet<TileBase> TileBases { get; set; } = null!;

        public DbSet<TileMap> TileMaps { get; set; } = null!;

        public DbSet<Tile> Tiles { get; set; } = null!;

        public QuartzContext(Connection connection) : base(connection) { }

        public QuartzContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElementMatchup>()
                .HasOne(pt => pt.AttackingElement)
                .WithMany(p => p.ElementMatchups);

            modelBuilder.Entity<ElementMatchup>()
                .HasOne(pt => pt.DefendingElement)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}