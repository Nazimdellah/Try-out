using Super_Cartes_Infinies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Models.Models
{
    public class OwnedCard
    {
        public int Id { get; set; }
      
        public virtual Card? Card  { get; set; }
        [JsonIgnore]
        public virtual Player? Player{ get; set; }
      
        

     

    }
}
