﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PersistentEntity;
using ReactiveUI;

namespace Quartz.Core.ObjectModel
{
    public class QuartzContext : DatabaseContext
    {
        public DbSet<Element> Elements { get; set; }

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

            modelBuilder.Entity<ReactiveObject>().Ignore(c => c.Changed);
            modelBuilder.Entity<ReactiveObject>().Ignore(c => c.Changing);
            modelBuilder.Entity<ReactiveObject>().Ignore(c => c.ThrownExceptions);
        }
    }
}