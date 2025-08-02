using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<SupplierMedicine> SupplierMedicines { get; set; } = new List<SupplierMedicine>();
}
