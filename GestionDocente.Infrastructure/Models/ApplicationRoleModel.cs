﻿using Microsoft.AspNetCore.Identity;

namespace GestionDocente.Infrastructure.Models
{
    public class ApplicationRoleModel : IdentityRole
    {
        public string? Description { get; set; }
    }
}
