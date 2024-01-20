using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookWebAPI.Models;

public partial class B1batchContext : DbContext
{
    public B1batchContext()
    {
    }

    public B1batchContext(DbContextOptions<B1batchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=B1Batch;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07FD6316CC");

            entity.Property(e => e.BookAuthor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BookPrice).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
