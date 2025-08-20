﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Feedback { get; set; }

    public string? Remarks { get; set; }

    public int? CreatedBy { get; set; }

    [JsonIgnore]
    public virtual Admin? CreatedByNavigation { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
