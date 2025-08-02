using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? MedicineId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Order? Order { get; set; }
}
