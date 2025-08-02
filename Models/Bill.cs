using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public int? CustomerId { get; set; }

    public DateTime? BillDate { get; set; }

    public string? PaymentMode { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public bool IsPaid { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual Customer? Customer { get; set; }

    public virtual Delivery? Delivery { get; set; }
}
