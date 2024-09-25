using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FullstackOpdracht.Areas.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
