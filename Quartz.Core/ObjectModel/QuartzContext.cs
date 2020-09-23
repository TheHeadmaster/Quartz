﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PersistentEntity;

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
                .WithMany(p => p.ElementMatchups)
                .HasForeignKey(pt => pt.AttackingElementID);

            modelBuilder.Entity<ElementMatchup>()
                .HasOne(pt => pt.DefendingElement)
                .WithMany(t => t.ElementMatchups)
                .HasForeignKey(pt => pt.DefendingElementID);
        }
    }
}