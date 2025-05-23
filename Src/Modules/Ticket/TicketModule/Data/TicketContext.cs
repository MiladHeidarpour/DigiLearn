﻿using Microsoft.EntityFrameworkCore;
using TicketModule.Data.Entities;

namespace TicketModule.Data;

class TicketContext:DbContext
{
    public TicketContext(DbContextOptions<TicketContext> option):base(option)
    {
        
    }

    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}