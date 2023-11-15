using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abeslamidze_Web.DAL.Entities
{
	public class ApplicationUser : IdentityUser
	{
        public byte[] AvatarImage { get; set; }
    }
}
