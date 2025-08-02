using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }

    [JsonIgnore]
    public virtual Order? Order { get; set; }
}
