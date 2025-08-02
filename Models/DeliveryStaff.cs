using System;
using System.Collections.Generic;

namespace Medical_Store.Models;

public partial class DeliveryStaff
{
    public int StaffId { get; set; }

    public string? FullName { get; set; }

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    public DateOnly? JoinedDate { get; set; }

    public bool? IsActive { get; set; }
}
