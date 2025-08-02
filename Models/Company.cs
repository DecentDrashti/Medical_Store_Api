using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
