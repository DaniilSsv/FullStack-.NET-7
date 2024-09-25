using FullstackOpdracht.Domains.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FullstackOpdracht.ViewModels
{
    public class CreateTicketVM
    {
        public int? matchID { get; set; }
        public string? Name { get; set; } // Name of the match
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
