using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Disease
{
    public int DiseaseId { get; set; }

    public string DiseaseName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MedicineDisease> MedicineDiseases { get; set; } = new List<MedicineDisease>();
}
