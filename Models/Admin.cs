using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
