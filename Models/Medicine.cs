using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    [JsonIgnore]
    public virtual Category? Category { get; set; }
    [JsonIgnore]
    public virtual Company? Company { get; set; }
    [JsonIgnore]
    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();
    [JsonIgnore]
    public virtual ICollection<MedicineDisease> MedicineDiseases { get; set; } = new List<MedicineDisease>();
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
     [JsonIgnore]
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
     [JsonIgnore]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
     [JsonIgnore]
    public virtual ICollection<SupplierMedicine> SupplierMedicines { get; set; } = new List<SupplierMedicine>();
}
//dropdown model
public class MedicineDropdown
{
    public int MedicineId { get; set; }
    public string MedicineName { get; set; } = null!;
    //public decimal Mrp { get; set; }
    //public decimal Ptr { get; set; }
    //public int? FreeQuantity { get; set; }
    //public bool IsPrescriptionRequired { get; set; }
}
