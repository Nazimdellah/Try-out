using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Models.Interfaces;

namespace Super_Cartes_Infinies.Models
{
    public class GameConfig : IModel
    {
        public GameConfig() { }

        public int Id { get; set; }

        [Display(Name = "Nbr cartes à piger avant de commencer la partie")]
        public int nbCardsToDraw { get; set; }
        [Display(Name = "Qti Mana reçu au début de chaque tour")]
        public int Mana { get; set; }
        public int nbMaxDecks { get; set; }
        public int nbMaxCartesDecks { get; set; }


    }
}
