using System;
using System.Collections.Generic;

namespace EcommerceAuthenticationCore.Models;

public partial class Shop
{
    public int Id { get; set; }

    public int ShopOldId { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string EmailAddres { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string TaxIdentificationNumber { get; set; } = null!;

    public bool IsApproved { get; set; }

    public bool IsCancel { get; set; }

    public int AdminApproved { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdateAt { get; set; }

    public DateTimeOffset? DeleteAt { get; set; }

    public virtual WebUser Owner { get; set; } = null!;
}
