using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class DeliveryDetail
{
    public int DeliveryDetailId { get; set; }

    public int? DeliveryId { get; set; }

    public int? MedicineId { get; set; }

    public int QuantityDelivered { get; set; }

    public DateTime? AddedOn { get; set; }

    [JsonIgnore]
    public virtual Delivery? Delivery { get; set; }

    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }
}
