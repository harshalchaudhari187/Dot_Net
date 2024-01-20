using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeMvcWithSecuirty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EmployeeMvcWithSecuirty.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly ActsDec2023Context _context;

        public PlayersController(ActsDec2023Context context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var actsDec2023Context = _context.Players.Include(p => p.Team);
            return View(await actsDec2023Context.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Dob,MinimumBattingAvg,MinimumWicketTaken,TeamId")] Player player)
        {
            
            var team = await _context.Teams
               .SingleOrDefaultAsync(m => m.Id == player.TeamId);
            
            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Today;
                int age = currentDate.Year - player.Dob.Value.Year;

                _context.Add(player);

                if (player.TeamId == 0) 
                {
                    ModelState.AddModelError("TeamId", "Please choose the team");
                }

                if (player.MinimumWicketTaken < team.WicketsTaken)
                {
                    ModelState.AddModelError("MinimumWicketTaken", "Minimum Wickets Taken for "+team.Name+" is "+team.WicketsTaken);
                }

                if (player.MinimumBattingAvg < team.BattingAvg)
                {
                    ModelState.AddModelError("MinimumBattingAvg", "Minimum Batting Average" + team.Name + " is " + team.BattingAvg);
                }

                if (age > team.MaxAge || age<18)
                {
                    ModelState.AddModelError("Dob", "Minimum Age should not be more than"+ team.MaxAge +".");
                }

                if (!ModelState.IsValid)
                {
                    ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
                    return View(player);
                }

                await _context.SaveChangesAsync();
                TempData["msg"] = "Registration Successfully";
                return RedirectToAction("Index", "Home", new {messgeId = "Your Id is : ", pId = player.Id });

            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Dob,MinimumBattingAvg,MinimumWicketTaken,TeamId")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Players == null)
            {
                return Problem("Entity set 'ActsDec2023Context.Players'  is null.");
            }
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
