using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }

    [JsonIgnore]
    public virtual Purchase? Purchase { get; set; }
}
