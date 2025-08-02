using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store.Models;

public partial class MedicalStoreContext : DbContext
{
    public MedicalStoreContext()
    {
    }

    public MedicalStoreContext(DbContextOptions<MedicalStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<DeliveryPayment> DeliveryPayments { get; set; }

    public virtual DbSet<DeliveryStaff> DeliveryStaffs { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineDisease> MedicineDiseases { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierMedicine> SupplierMedicines { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=DESKTOP-3I9H4UB\\SQLEXPRESS; database=MedicalStore; trusted_connection=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E8CB145483");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.FullName, "UQ__Admin__536C85E40907769B").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admin_Users");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bills__11F2FC4A00F59BE9");

            entity.HasIndex(e => e.InvoiceNumber, "UQ__Bills__D776E981E503B254").IsUnique();

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.BillDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.PaymentMode).HasMaxLength(20);
            entity.Property(e => e.TaxAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bills)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Bills__CustomerI__619B8048");
        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.BillDetailId).HasName("PK__BillDeta__793CAF75233408FD");

            entity.Property(e => e.BillDetailId).HasColumnName("BillDetailID");
            entity.Property(e => e.AddedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Added_On");
            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("FK__BillDetai__BillI__68487DD7");

            entity.HasOne(d => d.Medicine).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__BillDetai__Medic__693CA210");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BFCC80F60");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0CB5F510C").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4C36C64BBF");

            entity.ToTable("Company");

            entity.HasIndex(e => e.CompanyName, "UQ__Company__9BCE05DCD75034B8").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B887A2DA4C");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.CustomerNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_Users");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__Delivery__626D8FEEECEFCCCF");

            entity.ToTable("Delivery");

            entity.HasIndex(e => e.BillId, "UQ__Delivery__11F2FC4BD0B6FB9B").IsUnique();

            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DeliveredBy).HasMaxLength(100);
            entity.Property(e => e.DeliveryAddress).HasMaxLength(300);
            entity.Property(e => e.DeliveryDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DeliveryMethod).HasMaxLength(50);
            entity.Property(e => e.DeliveryStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Bill).WithOne(p => p.Delivery)
                .HasForeignKey<Delivery>(d => d.BillId)
                .HasConstraintName("FK__Delivery__BillID__6E01572D");

            entity.HasOne(d => d.Customer).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Delivery__Custom__6EF57B66");
        });

        modelBuilder.Entity<DeliveryPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Delivery__9B556A5888DB15FC");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BasePayment).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Bonus)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.DeliveryPerson).HasMaxLength(100);
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PetrolAllowance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Remarks).HasMaxLength(200);
            entity.Property(e => e.TotalPaid)
                .HasComputedColumnSql("(([BasePayment]+[Bonus])+[PetrolAllowance])", true)
                .HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Delivery).WithMany(p => p.DeliveryPayments)
                .HasForeignKey(d => d.DeliveryId)
                .HasConstraintName("FK__DeliveryP__Deliv__73BA3083");
        });

        modelBuilder.Entity<DeliveryStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Delivery__96D4AAF761E3624E");

            entity.ToTable("DeliveryStaff");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PK__Diseases__69B533A9906B15DD");

            entity.Property(e => e.DiseaseId).HasColumnName("DiseaseID");
            entity.Property(e => e.DiseaseName).HasMaxLength(100);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F2128F048977FB5");

            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Cdpercent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("CDPercent");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.FreeQuantity).HasDefaultValue(0);
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.MedicineName).HasMaxLength(100);
            entity.Property(e => e.Mrp)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("MRP");
            entity.Property(e => e.Ptr)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PTR");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Medicines__Categ__3E52440B");

            entity.HasOne(d => d.Company).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Medicines__Compa__3D5E1FD2");
        });

        modelBuilder.Entity<MedicineDisease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicine__3214EC27CC05B359");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiseaseId).HasColumnName("DiseaseID");
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

            entity.HasOne(d => d.Disease).WithMany(p => p.MedicineDiseases)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("FK__MedicineD__Disea__03F0984C");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineDiseases)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__MedicineD__Medic__02FC7413");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF5BE7BA73");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Feedback).HasMaxLength(500);
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Remarks).HasMaxLength(200);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Orders__CreatedB__571DF1D5");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__5535A963");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C0A4515DF");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__OrderDeta__Medic__5AEE82B9");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__59FA5E80");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130812A7DA806B");

            entity.Property(e => e.PrescriptionId).HasColumnName("PrescriptionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.UploadedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Prescript__Custo__123EB7A3");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__Prescript__Medic__1332DBDC");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BDEA5A26FB0");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.PurchaseDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Purchases__Creat__4E88ABD4");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Purchases__Suppl__4CA06362");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseDetailId).HasName("PK__Purchase__88C328D5EDEB1B39");

            entity.Property(e => e.PurchaseDetailId).HasColumnName("PurchaseDetailID");
            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__PurchaseD__Medic__52593CB8");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__PurchaseD__Purch__5165187F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A895F8F7A");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock__2C83A9E2D9820C6B");

            entity.ToTable("Stock");

            entity.Property(e => e.StockId).HasColumnName("StockID");
            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__Stock__MedicineI__5DCAEF64");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE6669469DA7D85");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.SupplierName).HasMaxLength(100);
        });

        modelBuilder.Entity<SupplierMedicine>(entity =>
        {
            entity.HasKey(e => e.SupplierMedicineId).HasName("PK__Supplier__0FCDB033705EE960");

            entity.Property(e => e.SupplierMedicineId).HasColumnName("SupplierMedicineID");
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplyPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.SupplierMedicines)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__SupplierM__Medic__49C3F6B7");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierMedicines)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__SupplierM__Suppl__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC552536F6");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__08B54D69");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
