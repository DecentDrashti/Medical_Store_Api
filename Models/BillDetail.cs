using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class BillDetail
{
    public int BillDetailId { get; set; }

    public int? BillId { get; set; }

    public int? MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    public DateTime? AddedOn { get; set; }

    [JsonIgnore]
    public virtual Bill? Bill { get; set; }

    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }
}
