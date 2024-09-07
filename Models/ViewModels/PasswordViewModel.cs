using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class PasswordViewModel
    {
        public int Id { get; set; }

        public string? Category { get; set; }

        public string App { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string? EncryptedPassword { get; set; } 
        public string? DecryptedPassword { get; set; }
    }

}
