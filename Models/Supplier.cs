using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
    [JsonIgnore]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    [JsonIgnore]
    public virtual ICollection<SupplierMedicine> SupplierMedicines { get; set; } = new List<SupplierMedicine>();
}
