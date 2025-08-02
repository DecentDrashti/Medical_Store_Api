using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? SupplierId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }
    [JsonIgnore]
    public virtual Admin? CreatedByNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }
}
