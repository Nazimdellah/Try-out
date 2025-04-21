using System.ComponentModel;
using Models.Interfaces;

namespace Super_Cartes_Infinies.Models
{
    public class Card:IModel
	{
		public Card() { }

		public int Id { get; set; }
		public string Name { get; set; } = "";
		public int Attack { get; set; }
		public int Health { get; set; }
		public int Cost { get; set; }
        public string ImageUrl { get; set; } = "";
    }
}

