﻿using Microsoft.AspNetCore.Identity;

namespace Nest.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int MyProperty { get; set; }
    }
}
