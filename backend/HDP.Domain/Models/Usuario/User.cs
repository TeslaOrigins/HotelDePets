using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Usuario
{
    public partial class User : IdentityUser
    {
        public string FullName { get; set; }

    }
}