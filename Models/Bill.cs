using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int OrderId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public int? CustomerId { get; set; }

    public DateTime? BillDate { get; set; }

    public string? PaymentMode { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public bool IsPaid { get; set; }

    [JsonIgnore]
    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    [JsonIgnore]
    public virtual Customer? Customer { get; set; }
    [JsonIgnore]
    public virtual Delivery? Delivery { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
}
public class BillDropdown
{
    public int BillId { get; set; }
    public bool IsPaid { get; set; }

}
