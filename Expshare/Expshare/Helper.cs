using Expshare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public static void UpdateTrenutnoStanje(this ExpShareContext context, List<Guid> korisnici)
        {
            using (var connection = new SqlConnection("Server=tcp:tandem.database.windows.net,1433;Database=ExpShare;User Id=tandem_admin;Password=Autoservis123"))
            {
                connection.Open();
                var command = new SqlCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "RecomputeTrenutnoStanjeKorisnika", 
                    Connection = connection
                };
                command.ExecuteNonQuery();
                 command = new SqlCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "RecomputeTrenutnoStanjeKorisnikaUGrupi", 
                    Connection = connection
                };
                command.ExecuteNonQuery();
                 command = new SqlCommand()
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "RecomputeStanjeIzmeduKorisnika", 
                    Connection =connection
                };
                command.ExecuteNonQuery();
            }
            //var oldTrenutnoStanjeKorisnika = context.TrenutnoStanjeKorisnika
            //    .Where(x => korisnici.Contains(x.IdKorisnik));
            //context.TrenutnoStanjeKorisnika.RemoveRange(oldTrenutnoStanjeKorisnika);
            //context.SaveChanges();
            //var oldTrenutnoStanjeKorisnikaUGrupi = context.TrenutnoStanjeKorisnikaUgrupi
            //    .Where(x => korisnici.Contains(x.IdKorisnik));
            //context.TrenutnoStanjeKorisnikaUgrupi.RemoveRange(oldTrenutnoStanjeKorisnikaUGrupi);
            //context.SaveChanges();
            //var oldStanjeIzmeduKorisnika = context.StanjeIzmeduKorisnika
            //    .Where(x => korisnici.Contains(x.IdKorisnik) || korisnici.Contains(x.IdDugovatelj));
            //context.StanjeIzmeduKorisnika.RemoveRange(oldStanjeIzmeduKorisnika);
            //context.SaveChanges();
            //var computedTrenutnoStanjeKorisnika = context.TrenutnoStanjeKorisnika
            //    .FromSql("SELECT * FROM ComputedTrenutnoStanjeKorisnika")
            //    .Where(x => korisnici.Contains(x.IdKorisnik))
            //    .ToList();
            //var computedTrenutnoStanjeKorisnikaUGrupi = context.TrenutnoStanjeKorisnikaUgrupi
            //    .FromSql("SELECT * FROM ComputedTrenutnoStanjeKorisnikaUGrupi")
            //    .Where(x => korisnici.Contains(x.IdKorisnik))
            //    .ToList();
            //var computedStanjeIzmeduKorisnika = context.StanjeIzmeduKorisnika
            //    .FromSql("SELECT * FROM ComputedStanjeIzmeduKorisnika")
            //    .Where(x => korisnici.Contains(x.IdKorisnik) || korisnici.Contains(x.IdDugovatelj))
            //    .ToList();

            //context.TrenutnoStanjeKorisnika.AddRange(computedTrenutnoStanjeKorisnika);
            //context.TrenutnoStanjeKorisnikaUgrupi.AddRange(computedTrenutnoStanjeKorisnikaUGrupi);
            //context.StanjeIzmeduKorisnika.AddRange(computedStanjeIzmeduKorisnika);
            ////context.TrenutnoStanjeKorisnika.FromSql("Exec RecomputeTrenutnoStanjeKorisnika");
            ////context.TrenutnoStanjeKorisnikaUgrupi.FromSql("Exec RecomputeTrenutnoStanjeKorisnikaUgrupi");
            ////context.StanjeIzmeduKorisnika.FromSql("Exec RecomputeStanjeIzmeduKorisnika");
            //context.SaveChanges();
        }

        public static string DodajIme(Microsoft.AspNetCore.Http.HttpContext context)
        {
            return context.User.Claims.Where(s => s.Type == ClaimTypes.Name)
                .Select(s => s.Value).Single();
        }
    }
}

