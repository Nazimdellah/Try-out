using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Super_Cartes_Infinies.Models;
using System.Text.Json.Serialization;

namespace Models.Models
{
  public class Deck
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        [ValidateNever]
        public virtual List<OwnedCard> CarteJoueurs { get; set; }
        public int PlayerId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public virtual Player Player { get; set; }
        public bool Courant { get; set; }
        public virtual List<OwnedCard> CarteSuprime { get; set; }
      
       




    }
}
