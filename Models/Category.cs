using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
