using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class UserToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool IsAdmin { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string AppName { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTimeOffset ExpiresAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdateAt { get; set; }

    public DateTimeOffset? LogoutAt { get; set; }
}
