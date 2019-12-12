using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt
{
    public class Seed
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("admin").Result == null)
            {
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Rol niet aangemaakt");
                }
            }
            if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "abc@xyz.com",
                    Email = "abc@xyz.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Welkom01!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            //Webredacteur
            if (roleManager.FindByNameAsync("webredacteur").Result == null)
            {
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("Webredacteur")).Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Rol niet aangemaakt");
                }
            }
            if (userManager.FindByEmailAsync("web@coma.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "web@coma.com",
                    Email = "web@coma.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Welkom01!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Webredacteur").Wait();
                }
            }

        }
    }
}
