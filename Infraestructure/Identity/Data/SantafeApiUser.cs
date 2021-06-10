using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SantafeApi.Infraestrucutre.Identity.Data
{
    // Add profile data for application users by adding properties to the SantafeApiUser class
    public class SantafeApiUser : IdentityUser
    {

        public bool HasAccess { get; set; }

        
    }

}

