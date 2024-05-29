using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LoginApplication.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
