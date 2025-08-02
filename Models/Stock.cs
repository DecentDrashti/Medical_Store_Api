using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int? MedicineId { get; set; }

    public string? BatchNo { get; set; }

    public int? QuantityAvailable { get; set; }

    public DateOnly? ExpiryDate { get; set; }
    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }
}
