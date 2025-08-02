using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class DeliveryPayment
{
    public int PaymentId { get; set; }

    public int? DeliveryId { get; set; }

    public string DeliveryPerson { get; set; } = null!;

    public decimal BasePayment { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? PetrolAllowance { get; set; }

    public decimal? TotalPaid { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? Remarks { get; set; }

    [JsonIgnore]
    public virtual Delivery? Delivery { get; set; }
}
