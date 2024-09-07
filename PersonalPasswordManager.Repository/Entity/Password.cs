using System;
using System.Collections.Generic;

namespace PersonalPasswordManager.Repository.Models;

public partial class Password
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public string App { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string EncryptedPassword { get; set; } = null!;
}
