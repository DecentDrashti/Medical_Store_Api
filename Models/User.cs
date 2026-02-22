using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Medical_Store.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }  // Default to Customer =2 in registration

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [JsonIgnore]
    public virtual Role? Role { get; set; } 
}
public class UserDropdown
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;

}
public class UserLogin
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RoleId { get; set; }
}

public class UserRegister
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RoleId { get; set; }
}

