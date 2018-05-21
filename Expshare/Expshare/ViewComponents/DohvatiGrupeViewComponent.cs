using Expshare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expshare.ViewComponents
{
    [ViewComponent]
    public class DohvatiGrupeViewComponent : ViewComponent
    {
        private readonly ExpShareContext _context;
        public DohvatiGrupeViewComponent(ExpShareContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid ID)
        {
            var grupe = await DohvatiGrupeAsync(ID);
            return View(grupe);
        }

        private Task<List<TrenutnoStanjeKorisnikaUgrupi>> DohvatiGrupeAsync(Guid ID)
        {

            var grupe =  _context.TrenutnoStanjeKorisnikaUgrupi
                .Include(x => x.IdGrupaNavigation)
                .Where(x => x.IdKorisnik.Equals(ID))
                .ToListAsync();
            return grupe;
        }
    }
}
