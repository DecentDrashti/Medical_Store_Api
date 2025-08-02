using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class MedicineDisease
{
    public int Id { get; set; }

    public int? MedicineId { get; set; }

    public int? DiseaseId { get; set; }

    public virtual Disease? Disease { get; set; }

    public virtual Medicine? Medicine { get; set; }
}
