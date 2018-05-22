using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Expshare.Models;
using Expshare.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expshare.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ExpshareController : Controller
    {
        private readonly ExpShareContext _context;
        public ExpshareController(ExpShareContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Home));
            }
            else
            {
                return View();
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return Json(new LoginAndRegisterResponse
            {
                Email = string.Empty,
                Nickname = string.Empty,
                IsAuthenticated = false,
                ErrorMessage = "Provjerite unesene parametre"
            });
            var korisnik = _context.Korisnik.Include(k => k.Lozinka)
                .Where(k => k.EmailKorisnik.ToLower() == model.Email.ToLower())
                .SingleOrDefault();
            if (korisnik == null)
            {
                return Json(new LoginAndRegisterResponse
                {
                    Email = model.Email,
                    IsAuthenticated = false,
                    ErrorMessage = "Neispravna email adresa ili lozinka."
                });
            }
            if (!Helper.CheckPassword(model.Lozinka, korisnik.Lozinka.LozinkaHash, korisnik.Lozinka.LozinkaSalt))
            {
                return Json(new LoginAndRegisterResponse
                {
                    Email = model.Email,
                    IsAuthenticated = false,
                    ErrorMessage = "Neispravna email adresa ili lozinka."
                });
            }
            await SignInAsync(korisnik, model.ZapamtiMe);

            return Json(new LoginAndRegisterResponse
            {
                Email = model.Email,
                IsAuthenticated = true
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid) return Json(new LoginAndRegisterResponse
            {
                Email = string.Empty,
                IsAuthenticated = false,
                ErrorMessage = "Provjerite unesene parametre!"
            });

            if (model.Lozinka != model.PotvrdiLozinku)
            {
                return Json(new LoginAndRegisterResponse
                {
                    Email = model.Email,
                    IsAuthenticated = false,
                    ErrorMessage = "Lozinke se ne podudaraju!"
                });
            }
            var korisnik = KreirajKorisnika(model);
            await SignInAsync(korisnik, model.ZapamtiMe);
            return Json(new LoginAndRegisterResponse
            {
                Email = model.Email,
                IsAuthenticated = true
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Home()
        {
            ViewData["user"] = Helper.DodajIme(HttpContext);
            return View();
        }

        public JsonResult DohvatiIDTrenutnogKorisnika()
        {
            return Json(User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => new { id = x.Value })
                .Single());
        }

        public JsonResult DohvatiUkupnoStanjeKorisnika(Guid id)
        {
            return Json(_context.TrenutnoStanjeKorisnika
                .Where(x => x.IdKorisnik == id)
                .Select(x => new { stanje = x.Stanje })
                .Single());
        }

        public JsonResult DohvatiGrupeIStanjaKorisnika(Guid id)
        {
            return Json(_context.TrenutnoStanjeKorisnikaUgrupi
                .Include(x => x.IdGrupaNavigation)
                .Include(x => x.IdKorisnikNavigation)
                .Where(x => x.IdGrupa == id)
                .Select(x => new
                {
                    x.IdGrupa,
                    x.IdGrupaNavigation.NazivGrupa,
                    x.IdKorisnik,
                    Email = x.IdKorisnikNavigation.EmailKorisnik,
                    x.IdKorisnikNavigation.Nickname,
                    x.Stanje
                })
                .ToList());
        }

        [HttpPost]
        public JsonResult KreirajUplatu([FromBody]KreirajUplatuViewModel model)
        {
            var transakcije = new List<Transakcija>();

            var korisniciZaUplatu = model.KorisniciZaUplatu;
            if (model.RaspodijeliSamnom)
            {
                korisniciZaUplatu.Add(model.IdKorisnik);
            }
            var pojedinacniIznos = model.Iznos / korisniciZaUplatu.Count;

            foreach (var korisnik in korisniciZaUplatu)
            {
                var transakcija = new Transakcija
                {
                    ID = Guid.NewGuid(),
                    IdGrupa = model.IdGrupa,
                    IdPlatitelj = model.IdKorisnik,
                    IdPrimatelj = korisnik,
                    Iznos = pojedinacniIznos,
                    Datum = DateTime.Now
                };
                transakcije.Add(transakcija);
            }

            _context.Transakcija.AddRange(transakcije);
            _context.SaveChanges();
            if (korisniciZaUplatu.All(x => x != model.IdKorisnik))
            {
                korisniciZaUplatu.Add(model.IdKorisnik);
            }
            _context.UpdateTrenutnoStanje(korisniciZaUplatu);
            return Json(model);
        }

        public JsonResult DohvatiStanjeIzmeduKorisnika(Guid idGrupa)
        {
            var trenutniKorisnik = User.Claims
            .Where(x => x.Type == ClaimTypes.NameIdentifier)
            .Select(x => new Guid(x.Value))
            .Single();
            var stanjeIzmeduKorisnika = _context.StanjeIzmeduKorisnika
                .Include(x => x.IdDugovateljNavigation)
                .Include(x => x.IdKorisnikNavigation)
                .Where(x => x.IdKorisnik == trenutniKorisnik && x.IdGrupa == idGrupa)
                .Select(x => new
                {
                    IdKorisnik = x.IdKorisnik, 
                    IdDugovatelj = x.IdDugovatelj, 
                    Email = x.IdDugovatelj != trenutniKorisnik ? 
                        x.IdDugovateljNavigation.EmailKorisnik : 
                        x.IdKorisnikNavigation.EmailKorisnik,
                    IdGrupa = x.IdGrupa, 
                    Stanje = x.Stanje
                })
                .ToList();
            return Json(stanjeIzmeduKorisnika);
        }

        public JsonResult DohvatiNazivGrupe(Guid id)
        {
            return Json(_context.Grupa
                .Where(x => x.ID == id)
                .Select(x => x.NazivGrupa).SingleOrDefault());
        }

        [HttpPost]
        public JsonResult DodajClana([FromBody]DodajClanaViewModel model)
        {
            var postojeciKorisnik = _context.Korisnik
                .Where(x => x.EmailKorisnik.ToLower() == model.Email.ToLower())
                .Where(x => x.Nickname == model.Nickname)
                .SingleOrDefault();
            if(postojeciKorisnik == null)
            {
                postojeciKorisnik = new Korisnik
                {
                    ID = Guid.NewGuid(),
                    EmailKorisnik = model.Email,
                    Nickname = model.Nickname
                };
                _context.Korisnik.Add(postojeciKorisnik);

            }

            _context.GrupaKorisnik.Add(new GrupaKorisnik
            {
                ID = Guid.NewGuid(),
                IdGrupa = model.IdGrupa,
                IdKorisnik = postojeciKorisnik.ID
            });
            _context.SaveChanges();

            _context.UpdateTrenutnoStanje(new List<Guid> { postojeciKorisnik.ID });

            return Json(string.Empty);
        }

        private async Task SignInAsync(Korisnik korisnik, bool zapamtiMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, korisnik.Nickname),
                new Claim(ClaimTypes.Email, korisnik.EmailKorisnik),
                new Claim(ClaimTypes.NameIdentifier, korisnik.ID.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = zapamtiMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private Korisnik KreirajKorisnika(RegisterViewModel model)
        {
            var postojeciKorisnik = _context.Korisnik
                .Include(x => x.Lozinka)
                .Where(x => x.EmailKorisnik.ToLower() == model.Email.ToLower())
                .Where(x => x.Nickname == model.Nickname)
                .SingleOrDefault();
            if (postojeciKorisnik != null && postojeciKorisnik.Lozinka != null) return null;
            Korisnik korisnik;
            if (postojeciKorisnik == null)
            {
                korisnik = new Korisnik
                {
                    ID = Guid.NewGuid(),
                    Nickname = model.Nickname,
                    EmailKorisnik = model.Email
                };
            }
            else
            {
                korisnik = postojeciKorisnik;
            }
            var passwordSalt = Helper.GenerateSalt();
            var passwordHash = Helper.GenerateSHA256Hash(model.Lozinka, passwordSalt);

            var lozinka = new Lozinka
            {
                ID = korisnik.ID,
                LozinkaHash = passwordHash,
                LozinkaSalt = passwordSalt
            };
            if(postojeciKorisnik == null)
            {
                _context.Korisnik.Add(korisnik);
            }
            _context.Lozinka.Add(lozinka);
            _context.SaveChanges();
            return korisnik;
        }

    }
}