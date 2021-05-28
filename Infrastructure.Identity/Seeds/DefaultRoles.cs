using Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            #region Anterior

            //Seed Roles
            //await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

            #endregion Anterior

            //Novo - Jesuino
            //Carregando a tabela Role

            #region Atualizado

            var defaultUser = new ApplicationUser();
            var role = await roleManager.FindByNameAsync(roleManager.Roles.ToString());

            foreach (var value in Enum.GetNames(typeof(Roles)))
            {
                if (await roleManager.FindByNameAsync(value) == null)
                {
                    role = new ApplicationRole() { Name = value };
                    await roleManager.CreateAsync(role);
                    await roleManager.AddClaimAsync(role, (new Claim(ClaimTypes.Role, value)));
                }
            }

            #endregion Atualizado
        }
    }
}