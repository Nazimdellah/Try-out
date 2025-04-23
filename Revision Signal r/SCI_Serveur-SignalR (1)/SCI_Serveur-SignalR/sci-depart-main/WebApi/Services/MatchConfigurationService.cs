using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Services
{
	public class MatchConfigurationService
    {
        private ApplicationDbContext _dbContext;

        public MatchConfigurationService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public int GetNbCardsToDraw() {
            // Stub: Pour l'intant, le stub retourne simplement 3
            // L'implémentation réelle devra la valeur configué
            // L'implémentation est la responsabilité de la personne en charge de la partie [Administration MVC]
            return 3;
        }

        public int GetNbManaPerTurn()
        {
            // Stub: Pour l'intant, le stub retourne simplement 2
            // L'implémentation réelle devra la valeur configué
            // L'implémentation est la responsabilité de la personne en charge de la partie [Administration MVC]
            return 2;
        }
    }
}

