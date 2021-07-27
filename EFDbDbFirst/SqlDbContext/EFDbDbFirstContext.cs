using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EFDbDbFirst.Models;

#nullable disable

namespace EFDbDbFirst.SqlDbContext
{
    public partial class EFDbDbFirstContext : DbContext
    {

        public EFDbDbFirstContext(DbContextOptions<EFDbDbFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-71N2F93\\MSSQLSERVER01;Initial Catalog=EFDbDbFirst; Trusted_Connection=True");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

//            modelBuilder.Entity<Address>(entity =>
//            {
//                entity.Property(e => e.AddressLine1).IsUnicode(false);

//                entity.Property(e => e.AddressLine2).IsUnicode(false);

//                entity.Property(e => e.Pincode).IsUnicode(false);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.Addresses)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_Address_User");
//            });

//            modelBuilder.Entity<Contact>(entity =>
//            {
//                entity.Property(e => e.Alternative).IsUnicode(false);

//                entity.Property(e => e.Mobile).IsUnicode(false);

//                entity.Property(e => e.Telephone).IsUnicode(false);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.Contacts)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_Contact_User");
//            });

//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.Property(e => e.FirstName).IsUnicode(false);

//                entity.Property(e => e.LastName).IsUnicode(false);

//                entity.Property(e => e.UserName).IsUnicode(false);
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
