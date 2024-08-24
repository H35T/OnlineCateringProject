using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineCateringProject.Models
{
    public partial class OnlineCateringContext : DbContext
    {
        public OnlineCateringContext()
        {
        }

        public OnlineCateringContext(DbContextOptions<OnlineCateringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Caterer> Caterers { get; set; } = null!;
        public virtual DbSet<CustOrder> CustOrders { get; set; } = null!;
        public virtual DbSet<CustOrderChild> CustOrderChildren { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerInvoice> CustomerInvoices { get; set; } = null!;
        public virtual DbSet<FavoriteCaterer> FavoriteCaterers { get; set; } = null!;
        public virtual DbSet<LoginMaster> LoginMasters { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuCategory> MenuCategories { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=HOANNC\\HOANNC;Database=OnlineCateringDB;User Id=sa;Password=Hoannbnc11;Encrypt=True;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caterer>(entity =>
            {
                entity.ToTable("Caterer");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.PinCode).HasMaxLength(10);
            });

            modelBuilder.Entity<CustOrder>(entity =>
            {
                entity.HasKey(e => e.OrderNo)
                    .HasName("PK__CustOrde__C3907C746595AC7A");

                entity.ToTable("CustOrder");

                entity.Property(e => e.CostPerPlate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(255);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.HasOne(d => d.Caterer)
                    .WithMany(p => p.CustOrders)
                    .HasForeignKey(d => d.CatererId)
                    .HasConstraintName("FK__CustOrder__Cater__440B1D61");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CustOrder__Custo__4316F928");
            });

            modelBuilder.Entity<CustOrderChild>(entity =>
            {
                entity.HasKey(e => new { e.OrderNo, e.MenuItemNo })
                    .HasName("PK__CustOrde__DB04468B2144DDE7");

                entity.ToTable("CustOrderChild");

                entity.HasOne(d => d.MenuItemNoNavigation)
                    .WithMany(p => p.CustOrderChildren)
                    .HasForeignKey(d => d.MenuItemNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustOrder__MenuI__47DBAE45");

                entity.HasOne(d => d.OrderNoNavigation)
                    .WithMany(p => p.CustOrderChildren)
                    .HasForeignKey(d => d.OrderNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustOrder__Order__46E78A0C");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.PinCode).HasMaxLength(10);
            });

            modelBuilder.Entity<CustomerInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo)
                    .HasName("PK__Customer__D796B22648B1CC95");

                entity.ToTable("CustomerInvoice");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerInvoices)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CustomerI__Custo__4BAC3F29");

                entity.HasOne(d => d.OrderNoNavigation)
                    .WithMany(p => p.CustomerInvoices)
                    .HasForeignKey(d => d.OrderNo)
                    .HasConstraintName("FK__CustomerI__Order__4AB81AF0");
            });

            modelBuilder.Entity<FavoriteCaterer>(entity =>
            {
                entity.HasKey(e => e.FavoriteId)
                    .HasName("PK__Favorite__CE74FAD561326370");

                entity.ToTable("FavoriteCaterer");

                entity.HasOne(d => d.Caterer)
                    .WithMany(p => p.FavoriteCaterers)
                    .HasForeignKey(d => d.CatererId)
                    .HasConstraintName("FK__FavoriteC__Cater__534D60F1");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FavoriteCaterers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__FavoriteC__Custo__52593CB8");
            });

            modelBuilder.Entity<LoginMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__LoginMas__1788CC4CE4DE1CF7");

                entity.ToTable("LoginMaster");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.UserType).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenuItemNo)
                    .HasName("PK__Menu__8943AFF021C5DE83");

                entity.ToTable("Menu");

                entity.Property(e => e.Describe).HasMaxLength(255);

                entity.Property(e => e.ItemName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Menu__CategoryId__3F466844");

                entity.HasOne(d => d.Caterer)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.CatererId)
                    .HasConstraintName("FK__Menu__CatererId__403A8C7D");
            });

            modelBuilder.Entity<MenuCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__MenuCate__19093A0B914EDD6D");

                entity.ToTable("MenuCategory");

                entity.Property(e => e.Category).HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
