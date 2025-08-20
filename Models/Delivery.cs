using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int BillId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly DeliveryDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public string DeliveryMethod { get; set; }
//eg:-    "Courier""Postal Service""In-store Pickup""Home Delivery""Express Shipping""Drone Delivery""Freight"

    public string DeliveryAddress { get; set; }

    public string DeliveryStatus { get; set; }

    public string DeliveredBy { get; set; }

    public string ContactNumber { get; set; }

    [JsonIgnore]
    public virtual Bill? Bill { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    [JsonIgnore]
    public virtual ICollection<DeliveryPayment> DeliveryPayments { get; set; } = new List<DeliveryPayment>();
}
