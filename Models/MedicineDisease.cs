using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class MedicineDisease
{
    public int Id { get; set; }

    public int? MedicineId { get; set; }

    public int? DiseaseId { get; set; }
    [JsonIgnore]
    public virtual Disease? Disease { get; set; }
    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }
}
