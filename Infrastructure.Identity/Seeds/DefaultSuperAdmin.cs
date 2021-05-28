using Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Mukesh",
                LastName = "Murugan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    #region Anterior

                    //await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());

                    #endregion Anterior

                    //Novo - Jesuino
                    //Carregando a tabela UserRoles

                    #region Atualizado

                    var roleClaims = new List<Claim>();

                    foreach (var value in Enum.GetNames(typeof(Roles)))
                        roleClaims.Add(new Claim(ClaimTypes.Role, value));

                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");

                    foreach (var value in Enum.GetNames(typeof(Roles)))
                        await userManager.AddToRoleAsync(defaultUser, value);

                    await userManager.AddClaimsAsync(defaultUser, roleClaims);

                    #endregion Atualizado
                }
            }
        }
    }
}