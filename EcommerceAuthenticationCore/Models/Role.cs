using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdateAt { get; set; }

    public DateTimeOffset? DeleteAt { get; set; }

    public virtual ICollection<AdminUser> Users { get; set; } = new List<AdminUser>();
}
