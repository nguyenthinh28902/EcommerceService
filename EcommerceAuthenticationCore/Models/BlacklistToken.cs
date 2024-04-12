using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class BlacklistToken
{
    public int Id { get; set; }

    public int UserTokenId { get; set; }

    public string Token { get; set; } = null!;

    public virtual UserToken UserToken { get; set; } = null!;
}
