using Microsoft.EntityFrameworkCore;
using SALESSYSTEM.Domain;
using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.DAL.Context
{
    public partial class SALESSYSDBContext : DbContext
    {
        public SALESSYSDBContext()
        {
        }

        public SALESSYSDBContext(DbContextOptions<SALESSYSDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


        public virtual DbSet<Business> Businesses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Configuration> Configurations { get; set; } = null!;
        public virtual DbSet<CorrelativeNumber> CorrelativeNumbers { get; set; } = null!;
        public virtual DbSet<DocumentTypeSale> DocumentTypeSales { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleMenu> RoleMenus { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SaleDetail> SaleDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>(entity =>
            {
                entity.HasKey(e => e.IdBusiness)
                    .HasName("PK__Business__79BC445D0DB3CAE1");

                entity.ToTable("Business");

                entity.Property(e => e.IdBusiness)
                    .ValueGeneratedNever()
                    .HasColumnName("idBusiness");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.CurrencySymbol)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("currencySymbol");

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentNumber");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.LogoName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("logoName");

                entity.Property(e => e.LogoUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("logoUrl");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.TaxPercentage)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("taxPercentage");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__Category__79D361B61FD47F28");

                entity.ToTable("Category");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Configuration");

                entity.Property(e => e.Property)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("property");

                entity.Property(e => e.Resource)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("resource");

                entity.Property(e => e.Value)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<CorrelativeNumber>(entity =>
            {
                entity.HasKey(e => e.IdCorrelativeNumber)
                    .HasName("PK__Correlat__D71CDFB084E3AFEE");

                entity.ToTable("CorrelativeNumber");

                entity.Property(e => e.IdCorrelativeNumber).HasColumnName("idCorrelativeNumber");

                entity.Property(e => e.DigitCount).HasColumnName("digitCount");

                entity.Property(e => e.LastNumber).HasColumnName("lastNumber");

                entity.Property(e => e.LastUpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdateDate");

                entity.Property(e => e.Management)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("management");
            });

            modelBuilder.Entity<DocumentTypeSale>(entity =>
            {
                entity.HasKey(e => e.IdDocumentTypeSale)
                    .HasName("PK__Document__7C977251EBFF487F");

                entity.ToTable("DocumentTypeSale");

                entity.Property(e => e.IdDocumentTypeSale).HasColumnName("idDocumentTypeSale");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__C26AF4830F2A1CC3");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.ActionPage)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("actionPage");

                entity.Property(e => e.Controller)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("controller");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Icon)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("icon");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Menu__parentId__49C3F6B7");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Product__5EEC79D186719C93");

                entity.ToTable("Product");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("barcode");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("brand");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imageName");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK__Product__idCateg__5BE2A6F2");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Role__E5045C54AC5D4708");

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.HasKey(e => e.IdRoleMenu)
                    .HasName("PK__RoleMenu__50A394F91168AE13");

                entity.ToTable("RoleMenu");

                entity.Property(e => e.IdRoleMenu).HasColumnName("idRoleMenu");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RoleMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK__RoleMenu__idMenu__5165187F");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.RoleMenus)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK__RoleMenu__idRole__5070F446");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.IdSale)
                    .HasName("PK__Sale__C4AEB198E4032E71");

                entity.ToTable("Sale");

                entity.Property(e => e.IdSale).HasColumnName("idSale");

                entity.Property(e => e.ClientDocument)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("clientDocument");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("clientName");

                entity.Property(e => e.IdDocumentTypeSale).HasColumnName("idDocumentTypeSale");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SaleNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("saleNumber");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("subTotal");

                entity.Property(e => e.TaxTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("taxTotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdDocumentTypeSaleNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IdDocumentTypeSale)
                    .HasConstraintName("FK__Sale__idDocument__6477ECF3");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__Sale__idUser__656C112C");
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.HasKey(e => e.IdSaleDetail)
                    .HasName("PK__SaleDeta__B23385CD8BC0D9F8");

                entity.ToTable("SaleDetail");

                entity.Property(e => e.IdSaleDetail).HasColumnName("idSaleDetail");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdSale).HasColumnName("idSale");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductBrand)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("productBrand");

                entity.Property(e => e.ProductCategory)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("productCategory");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("productDescription");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.IdSale)
                    .HasConstraintName("FK__SaleDetai__idSal__693CA210");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__3717C98209C32032");

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

             

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK__User__idRole__5535A963");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}