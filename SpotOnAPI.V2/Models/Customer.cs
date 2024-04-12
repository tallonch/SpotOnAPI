using System;
using System.Collections.Generic;

namespace SpotOnAPI.V2.Models;

public partial class Customer
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string LastName { get; set; } = null!;

    public string? FirstName { get; set; }
}
