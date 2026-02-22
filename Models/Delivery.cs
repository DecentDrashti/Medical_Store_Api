using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int BillId { get; set; }

    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string? DeliveryMethod { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? DeliveryStatus { get; set; }

    public string? DeliveredBy { get; set; }

    public string? ContactNumber { get; set; }

    [JsonIgnore]
    public virtual Bill Bill { get; set; } = null!;

    [JsonIgnore]
    public virtual Customer Customer { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

    [JsonIgnore]
    public virtual ICollection<DeliveryPayment> DeliveryPayments { get; set; } = new List<DeliveryPayment>();
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
}
