using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class PurchaseDetail
{
    public int PurchaseDetailId { get; set; }

    public int? PurchaseId { get; set; }

    public int? MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string? BatchNo { get; set; }

    public DateOnly? MfgDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Purchase? Purchase { get; set; }
}
