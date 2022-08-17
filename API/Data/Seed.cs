using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext ctx)
        {
            // check if there are usesr

            if (await ctx.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData); // read data from file and put it into a list of appuser objects i.e. populate those fields

            //next we will iterate through the list and store each object into our AppUser table in our database.

            foreach (var u in users)
            {

                using var hmac = new HMACSHA512();
                u.UserName = u.UserName.ToLower();
                u.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")); //  create the same pass for all users :)
                u.PasswordSalt = hmac.Key;
                ctx.Users.Add(u);
            }

            await ctx.SaveChangesAsync();
        }

    }
}