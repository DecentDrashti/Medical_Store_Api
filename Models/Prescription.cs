using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int? CustomerId { get; set; }

    public int? MedicineId { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime? UploadedOn { get; set; }

    public bool? IsApproved { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    [JsonIgnore]
    public virtual Medicine? Medicine { get; set; }
}
