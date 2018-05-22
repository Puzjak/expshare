using Expshare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Expshare
{
    public static class Helper
    {
        #region Hashing
        public static string GenerateSalt(int size = 16)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            var salt = Convert.ToBase64String(buffer);
            return salt;
        }

        public static string GenerateSHA256Hash(string password, string salt)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(salt + password);
            var sha256Generator = new SHA256Managed();
            var hash = sha256Generator.ComputeHash(bytes);
            var hashString = Convert.ToBase64String(hash);
            return hashString;
        }
        #endregion

        public static bool CheckPassword(string password, string passwordHash, string salt)
        {
            var newPasswordHash = GenerateSHA256Hash(password, salt);
            return newPasswordHash == passwordHash;
        }

        public static void UpdateTrenutnoStanje(this ExpShareContext context, List<Guid> korisnici, Guid idGrupa)
        {
            var oldTrenutnoStanjeKorisnika = context.TrenutnoStanjeKorisnika
                .Where(x => korisnici.Contains(x.IdKorisnik));
            context.TrenutnoStanjeKorisnika.RemoveRange(oldTrenutnoStanjeKorisnika);
            context.SaveChanges();
            var oldTrenutnoStanjeKorisnikaUGrupi = context.TrenutnoStanjeKorisnikaUgrupi
                .Where(x => korisnici.Contains(x.IdKorisnik) && x.IdGrupa == idGrupa);
            context.TrenutnoStanjeKorisnikaUgrupi.RemoveRange(oldTrenutnoStanjeKorisnikaUGrupi);
            context.SaveChanges();
            var oldStanjeIzmeduKorisnika = context.StanjeIzmeduKorisnika
                .Where(x => korisnici.Contains(x.IdKorisnik) || korisnici.Contains(x.IdDugovatelj) && x.IdGrupa == idGrupa);
            context.StanjeIzmeduKorisnika.RemoveRange(oldStanjeIzmeduKorisnika);
            context.SaveChanges();
            var computedTrenutnoStanjeKorisnika = context.TrenutnoStanjeKorisnika
                .FromSql("SELECT * FROM ComputedTrenutnoStanjeKorisnika")
                .Where(x => korisnici.Contains(x.IdKorisnik))
                .ToList();
            var computedTrenutnoStanjeKorisnikaUGrupi = context.TrenutnoStanjeKorisnikaUgrupi
                .FromSql("SELECT * FROM ComputedTrenutnoStanjeKorisnikaUGrupi")
                .Where(x => korisnici.Contains(x.IdKorisnik) && x.IdGrupa == idGrupa)
                .ToList();
            var computedStanjeIzmeduKorisnika = context.StanjeIzmeduKorisnika
                .FromSql("SELECT * FROM ComputedStanjeIzmeduKorisnika")
                .Where(x => korisnici.Contains(x.IdKorisnik) || korisnici.Contains(x.IdDugovatelj) && x.IdGrupa == idGrupa)
                .ToList();

            context.TrenutnoStanjeKorisnika.AddRange(computedTrenutnoStanjeKorisnika);
            context.TrenutnoStanjeKorisnikaUgrupi.AddRange(computedTrenutnoStanjeKorisnikaUGrupi);
            context.StanjeIzmeduKorisnika.AddRange(computedStanjeIzmeduKorisnika);
            context.SaveChanges();
        }

        public static string DodajIme(Microsoft.AspNetCore.Http.HttpContext context)
        {
            return context.User.Claims.Where(s => s.Type == ClaimTypes.Name)
                .Select(s => s.Value).Single();
        }
    }
}

