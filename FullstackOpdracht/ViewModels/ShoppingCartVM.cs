using FullstackOpdracht.Domains.Entities;
using System.ComponentModel.DataAnnotations;

namespace FullstackOpdracht.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM>? Cart { get; set; }

    }
    public class CartVM
    {
        public int? CartId { get; set; }
        public string? Naam { get; set; }
        public int? Aantal { get; set; }
        public float Prijs { get; set; }
        public int? MatchId { get; set; } // bij abonnement is dit null
        public int? TeamId { get; set; } // bij ticket is dit null
        [Display(Name="Vak")]
        public Section Section { get; set; }
        public Ring Ring {  get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
