using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Migrations
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync (UserManager<Core.Entities.Identity.AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mele",
                    Email = "mel@test.com",
                    UserName = "mel@test.com",
                    Address = new Address
                    {
                        FirstName = "Mel",
                        LastName = "MeleseGashaw",
                        Street = "Addis Ababa",
                        State = "Addis Ababa",
                        ZipCode = "2211",
                    }
                };
                await userManager.CreateAsync(user,"P@55w0rd2211");
            }
        }
    }
}
