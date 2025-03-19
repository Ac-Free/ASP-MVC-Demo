using Microsoft.EntityFrameworkCore;
using MyItem.Models.Entities;

namespace MyItem.Data;

public partial class BookDbContext : DbContext
{
    public BookDbContext()
    {
    }

    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MyItem.Models.Entities.Book> Books { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Book;User ID=sa;Password=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<MyItem.Models.Entities.Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC075986DF8C");

            entity.ToTable("Book");

            entity.Property(e => e.BookName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0716C657FE");

            entity.ToTable("User");

            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Pwd)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("密码");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("用户名");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
