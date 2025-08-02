using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? SupplierId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Admin? CreatedByNavigation { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual Supplier? Supplier { get; set; }
}
