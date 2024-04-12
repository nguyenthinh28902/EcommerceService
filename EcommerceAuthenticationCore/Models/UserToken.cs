using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class UserToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool IsAdmin { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTimeOffset ExpiresAt { get; set; }

    public virtual ICollection<BlacklistToken> BlacklistTokens { get; set; } = new List<BlacklistToken>();
}
