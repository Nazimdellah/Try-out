using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Super_Cartes_Infinies.Data;

namespace Super_Cartes_Infinies.Controllers
{
    [Authorize(Roles = "admin")]
    public class StartingCardsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public StartingCardsController(ApplicationDbContext context)
        {
            _context = context;
        } 

        // GET: StartingCards
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StartingCards.Include(s => s.Card).OrderBy(s=>s.Card.Name).ToListAsync();
            return View(await applicationDbContext);
        }


        // GET: StartingCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startingCard = await _context.StartingCards
                .Include(s => s.Card)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (startingCard == null)
            {
                return NotFound();
            }

            return View(startingCard);
        }

        // POST: StartingCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var startingCard = await _context.StartingCards.FindAsync(id);
            if (startingCard != null)
            {
                _context.StartingCards.Remove(startingCard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StartingCardExists(int id)
        {
            return _context.StartingCards.Any(e => e.Id == id);
        }
    }
}
