using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expshare.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Expshare.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class GrupaController : Controller
    {
        private readonly ExpShareContext _context;

        public GrupaController(ExpShareContext context)
        {
            _context = context;
        }

        // GET: Grupa
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Grupa.ToListAsync());
        //}

        // GET: Grupa/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grupa = await _context.Grupa
        //        .SingleOrDefaultAsync(m => m.ID == id);
        //    if (grupa == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grupa);
        //}

        // GET: Grupa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NazivGrupa")] Grupa grupa)
        {
            if (ModelState.IsValid)
            {


                var id = User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => new Guid(x.Value))
                .Single();
                var korisnik = await _context.Korisnik.SingleAsync(k => k.ID == id);

                grupa.ID = Guid.NewGuid();
                var par = new GrupaKorisnik
                {
                    ID = Guid.NewGuid(),
                    IdGrupa = grupa.ID,
                    IdKorisnik = korisnik.ID
                };

                var stanje = new TrenutnoStanjeKorisnikaUgrupi
                {
                    IdKorisnik = par.IdKorisnik,
                    IdGrupa = grupa.ID,
                    Stanje = 0.0M
                };

                _context.Add(grupa); //dodajem grupu
                _context.Add(par); //dodajem par
                _context.Add(stanje); //dodajem novo stanje prema grupi
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","ExpShare");
            }
            return View(grupa);
        }

        // GET: Grupa/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupa = await _context.Grupa.SingleOrDefaultAsync(m => m.ID == id);
            if (grupa == null)
            {
                return NotFound();
            }
            return View(grupa);
        }

        // POST: Grupa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,NazivGrupa")] Grupa grupa)
        {
            if (id != grupa.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupaExists(grupa.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ExpShare");
            }
            return View(grupa);
        }

        // GET: Grupa/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupa = await _context.Grupa
                .SingleOrDefaultAsync(m => m.ID == id);
            if (grupa == null)
            {
                return NotFound();
            }

            return View(grupa);
        }

        // POST: Grupa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grupa = await _context.Grupa.SingleOrDefaultAsync(m => m.ID == id);
            _context.Grupa.Remove(grupa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ExpShare");
        }

        private bool GrupaExists(Guid id)
        {
            return _context.Grupa.Any(e => e.ID == id);
        }
    }
}
