using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class SupplierMedicine
{
    public int SupplierMedicineId { get; set; }

    public int? SupplierId { get; set; }

    public int? MedicineId { get; set; }

    public decimal? SupplyPrice { get; set; }

    public int? AvailableQuantity { get; set; }

    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }

    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }
}
