using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Controllers
{
    [Authorize(Roles = "admin")]
    public class GameConfigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameConfigsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameConfigs
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameConfig.ToListAsync());
        }

        // GET: GameConfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameConfig = await _context.GameConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameConfig == null)
            {
                return NotFound();
            }

            return View(gameConfig);
        }

        // GET: GameConfigs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nbCardsToDraw,Mana")] GameConfig gameConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameConfig);
        }

        // GET: GameConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameConfig = await _context.GameConfig.FindAsync(id);
            if (gameConfig == null)
            {
                return NotFound();
            }
            return View(gameConfig);
        }

        // POST: GameConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nbCardsToDraw,Mana")] GameConfig gameConfig)
        {
            if (id != gameConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameConfigExists(gameConfig.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameConfig);
        }

        // GET: GameConfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameConfig = await _context.GameConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameConfig == null)
            {
                return NotFound();
            }

            return View(gameConfig);
        }

        // POST: GameConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameConfig = await _context.GameConfig.FindAsync(id);
            if (gameConfig != null)
            {
                _context.GameConfig.Remove(gameConfig);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameConfigExists(int id)
        {
            return _context.GameConfig.Any(e => e.Id == id);
        }
    }
}
