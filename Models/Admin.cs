using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual User User { get; set; } = null!;
}
