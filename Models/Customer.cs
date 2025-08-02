using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    [JsonIgnore]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [JsonIgnore]
    public virtual User CustomerNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
