﻿using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class WebUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string EmailAddres { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTimeOffset? EndLockAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdateAt { get; set; }

    public DateTimeOffset? DeleteAt { get; set; }

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
