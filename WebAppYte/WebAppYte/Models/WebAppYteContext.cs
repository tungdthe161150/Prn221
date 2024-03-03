using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppYte.Models
{
    public partial class WebAppYteContext : DbContext
    {
        public WebAppYteContext()
        {
        }

        public WebAppYteContext(DbContextOptions<WebAppYteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BenhAn> BenhAns { get; set; }
        public virtual DbSet<GioiTinh> GioiTinhs { get; set; }
        public virtual DbSet<HoiDap> HoiDaps { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<LichKham> LichKhams { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<QuanTri> QuanTris { get; set; }
        public virtual DbSet<Solieucovid> Solieucovids { get; set; }
        public virtual DbSet<TinhThanh> TinhThanhs { get; set; }
        public virtual DbSet<Tintuc> Tintucs { get; set; }
        public virtual DbSet<TrungTamGanNhat> TrungTamGanNhats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = WebAppYte; uid=sa;pwd=123456;Trusted_Connection=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenhAn>(entity =>
            {
                entity.HasKey(e => e.IdbenhAn);

                entity.ToTable("BenhAn");

                entity.Property(e => e.IdbenhAn).HasColumnName("IDBenhAn");

                entity.Property(e => e.BacSyKham).HasMaxLength(50);

                entity.Property(e => e.DonThuoc).HasMaxLength(200);

                entity.Property(e => e.IdlichKham).HasColumnName("IDLichKham");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.ThoiGian).HasColumnType("smalldatetime");

                entity.HasOne(d => d.IdlichKhamNavigation)
                    .WithMany(p => p.BenhAns)
                    .HasForeignKey(d => d.IdlichKham)
                    .HasConstraintName("FK_BenhAn_LichKham");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.BenhAns)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK_BenhAn_NguoiDung");
            });

            modelBuilder.Entity<GioiTinh>(entity =>
            {
                entity.HasKey(e => e.IdgioiTinh);

                entity.ToTable("GioiTinh");

                entity.Property(e => e.IdgioiTinh).HasColumnName("IDGioiTinh");

                entity.Property(e => e.GioiTinh1)
                    .HasMaxLength(6)
                    .HasColumnName("GioiTinh");
            });

            modelBuilder.Entity<HoiDap>(entity =>
            {
                entity.HasKey(e => e.Idhoidap);

                entity.ToTable("HoiDap");

                entity.Property(e => e.Idhoidap).HasColumnName("IDHoidap");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdquanTri).HasColumnName("IDQuanTri");

                entity.Property(e => e.NgayGui)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TrangThai).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.HoiDaps)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK_HoiDap_NguoiDung");

                entity.HasOne(d => d.IdquanTriNavigation)
                    .WithMany(p => p.HoiDaps)
                    .HasForeignKey(d => d.IdquanTri)
                    .HasConstraintName("FK_HoiDap_QuanTri");
            });

            modelBuilder.Entity<Khoa>(entity =>
            {
                entity.HasKey(e => e.Idkhoa);

                entity.ToTable("Khoa");

                entity.Property(e => e.Idkhoa).HasColumnName("IDKhoa");

                entity.Property(e => e.TenKhoa).HasMaxLength(50);
            });

            modelBuilder.Entity<LichKham>(entity =>
            {
                entity.HasKey(e => e.IdlichKham);

                entity.ToTable("LichKham");

                entity.Property(e => e.IdlichKham).HasColumnName("IDLichKham");

                entity.Property(e => e.BatDau).HasColumnType("smalldatetime");

                entity.Property(e => e.ChuDe).HasMaxLength(100);

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.IdquanTri).HasColumnName("IDQuanTri");

                entity.Property(e => e.KetQuaKham).HasMaxLength(200);

                entity.Property(e => e.KetThuc).HasColumnType("smalldatetime");

                entity.Property(e => e.MoTa).HasMaxLength(300);

                entity.Property(e => e.ZoomInfo).HasMaxLength(100);

                entity.HasOne(d => d.IdnguoiDungNavigation)
                    .WithMany(p => p.LichKhams)
                    .HasForeignKey(d => d.IdnguoiDung)
                    .HasConstraintName("FK_LichKham_NguoiDung");

                entity.HasOne(d => d.IdquanTriNavigation)
                    .WithMany(p => p.LichKhams)
                    .HasForeignKey(d => d.IdquanTri)
                    .HasConstraintName("FK_LichKham_QuanTri");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.IdnguoiDung);

                entity.ToTable("NguoiDung");

                entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.IdgioiTinh).HasColumnName("IDGioiTinh");

                entity.Property(e => e.Idtinh).HasColumnName("IDTinh");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.NhomMau)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.SoCmnd).HasColumnName("SoCMND");

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.IdgioiTinhNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.IdgioiTinh)
                    .HasConstraintName("FK_NguoiDung_GioiTinh");

                entity.HasOne(d => d.IdtinhNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.Idtinh)
                    .HasConstraintName("FK_NguoiDung_TinhThanh");
            });

            modelBuilder.Entity<QuanTri>(entity =>
            {
                entity.HasKey(e => e.IdquanTri);

                entity.ToTable("QuanTri");

                entity.Property(e => e.IdquanTri).HasColumnName("IDQuanTri");

                entity.Property(e => e.AnhBia)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Idkhoa).HasColumnName("IDKhoa");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.ThongtinZoom).HasMaxLength(100);

                entity.Property(e => e.TrinhDo).HasMaxLength(50);

                entity.HasOne(d => d.IdkhoaNavigation)
                    .WithMany(p => p.QuanTris)
                    .HasForeignKey(d => d.Idkhoa)
                    .HasConstraintName("FK_QuanTri_Khoa");
            });

            modelBuilder.Entity<Solieucovid>(entity =>
            {
                entity.HasKey(e => e.Idthongkecovid);

                entity.ToTable("Solieucovid");

                entity.Property(e => e.Idthongkecovid).HasColumnName("IDThongkecovid");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Quocgia).HasMaxLength(50);
            });

            modelBuilder.Entity<TinhThanh>(entity =>
            {
                entity.HasKey(e => e.Idtinh);

                entity.ToTable("TinhThanh");

                entity.Property(e => e.Idtinh).HasColumnName("IDTinh");

                entity.Property(e => e.TenTinh).HasMaxLength(30);
            });

            modelBuilder.Entity<Tintuc>(entity =>
            {
                entity.HasKey(e => e.Idtintuc);

                entity.ToTable("Tintuc");

                entity.Property(e => e.Idtintuc).HasColumnName("IDTintuc");

                entity.Property(e => e.Hinhanh).HasMaxLength(100);

                entity.Property(e => e.Ngaydang)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TheLoai)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TrungTamGanNhat>(entity =>
            {
                entity.HasKey(e => e.IdtrungTam);

                entity.ToTable("TrungTamGanNhat");

                entity.Property(e => e.IdtrungTam).HasColumnName("IDTrungTam");

                entity.Property(e => e.Idtinh).HasColumnName("IDTinh");

                entity.Property(e => e.TenTrungTam).HasMaxLength(100);

                entity.HasOne(d => d.IdtinhNavigation)
                    .WithMany(p => p.TrungTamGanNhats)
                    .HasForeignKey(d => d.Idtinh)
                    .HasConstraintName("FK_TrungTamGanNhat_TinhThanh");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
