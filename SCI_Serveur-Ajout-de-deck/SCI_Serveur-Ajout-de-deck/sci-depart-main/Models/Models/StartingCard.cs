using Super_Cartes_Infinies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;

namespace Models.Models
{
    public class StartingCard : IModel
    {
        public StartingCard()
        {
        }
        public StartingCard(Card c)
        {
            Card = c;
        }

        public int Id { get; set; }
        public virtual Card Card { get; set; }

    }
}

