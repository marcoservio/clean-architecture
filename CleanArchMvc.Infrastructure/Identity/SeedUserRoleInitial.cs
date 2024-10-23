using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infrastructure.Enums;

using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infrastructure.Identity;

public class SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public void SeedRoles()
    {
        if (_roleManager.RoleExistsAsync(nameof(Roles.User)).Result) {
            var role = new IdentityRole();
            role.Name = nameof(Roles.User);
            role.NormalizedName = nameof(Roles.User).ToUpper();
            _ = _roleManager.CreateAsync(role).Result;
        }

        if (_roleManager.RoleExistsAsync(nameof(Roles.Admin)).Result)
        {
            var role = new IdentityRole();
            role.Name = nameof(Roles.Admin);
            role.NormalizedName = nameof(Roles.Admin).ToUpper();
            _ = _roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUser()
    {
        if(_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "usuario@localhost";
            user.Email = "usuario@localhost";
            user.NormalizedUserName = "USUARIO@LOCALHOST";
            user.NormalizedEmail = "USUARIO@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = _userManager.CreateAsync(user, "Numsey#2021").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, nameof(Roles.User)).Wait();
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "admin@localhost";
            user.Email = "admin@localhost";
            user.NormalizedUserName = "ADMIN@LOCALHOST";
            user.NormalizedEmail = "ADMIN@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = _userManager.CreateAsync(user, "Numsey#2021").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, nameof(Roles.Admin)).Wait();
        }
    }
}
