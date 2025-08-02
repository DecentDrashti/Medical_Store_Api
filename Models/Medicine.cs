using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public int? CompanyId { get; set; }

    public int? CategoryId { get; set; }

    public string? Manufacturer { get; set; }

    public string? Type { get; set; }

    public DateOnly MfgDate { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public decimal Ptr { get; set; }

    public decimal Mrp { get; set; }

    public decimal? Cdpercent { get; set; }

    public int? FreeQuantity { get; set; }

    public bool IsPrescriptionRequired { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual Category? Category { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<MedicineDisease> MedicineDiseases { get; set; } = new List<MedicineDisease>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<SupplierMedicine> SupplierMedicines { get; set; } = new List<SupplierMedicine>();
}
