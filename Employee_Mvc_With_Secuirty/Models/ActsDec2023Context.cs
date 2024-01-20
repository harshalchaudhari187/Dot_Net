using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMvcWithSecuirty.Models;

public partial class ActsDec2023Context : DbContext
{
    public ActsDec2023Context()
    {
    }

    public ActsDec2023Context(DbContextOptions<ActsDec2023Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MsSqlLocalDb;Initial Catalog=ActsDec2023;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0729F30617");

            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MinimumBattingAvg).HasColumnName("Minimum_batting_avg");
            entity.Property(e => e.MinimumWicketTaken).HasColumnName("Minimum_wicket_taken");
            entity.Property(e => e.TeamId).HasColumnName("Team_id");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Players_ToTable");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teams__3214EC075DF400C6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.BattingAvg).HasColumnName("batting_avg");
            entity.Property(e => e.MaxAge).HasColumnName("max_age");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Owner)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WicketsTaken).HasColumnName("wickets_taken");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
