using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FullstackOpdracht.ViewModels
{
    public class CreateMembershipVM
    {
        public int? TeamId { get; set; }
        public string Name { get; set; }

        [Required]
        public int? Section { get; set; } // Selected section
        [Required]
        public int? Ring { get; set; } // Selected ring

        // List of sections and rings for the dropdown menus
        [Required]
        public IEnumerable<SelectListItem>? Sections { get; set; }
        [Required]
        public IEnumerable<SelectListItem>? Rings { get; set; }
    }
}
