using System;
using System.Collections.Generic;

namespace SpotOnAPI.V2.Models;

public partial class Collar
{
    public int UserId { get; set; }

    public Guid CollarId { get; set; }

    public string? Nickname { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public DateTime? Timestamp { get; set; }
}
