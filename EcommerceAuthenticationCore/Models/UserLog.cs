using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class UserLog
{
    public int LogId { get; set; }

    public int UserId { get; set; }

    public bool IsAdmin { get; set; }

    public string UserAction { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public int RecordId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
