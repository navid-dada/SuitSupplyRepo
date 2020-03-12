using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Data
{
    public static class IdentityDataInitializer
    {
        private const string SalesRepRoleName = "SalesRep"; 
        private const string TailorRoleName = "Tailor"; 
        private const string SalesRepUserName = "salesrep@suitsupply.com"; 
        private const string TailorUserName = "tailor@suitsupply.com"; 
        public static  async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            if (! await roleManager.RoleExistsAsync(SalesRepRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(SalesRepRoleName));
            }

            if (!await roleManager.RoleExistsAsync(TailorRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(TailorRoleName));
            }

            var tailorUser = await userManager.FindByEmailAsync(TailorUserName);
            if (tailorUser == null)
            {
                tailorUser = new IdentityUser
                {
                    UserName = TailorUserName,
                    Email = TailorUserName
                };
                await userManager.CreateAsync(tailorUser, "a123456");
                await userManager.AddToRoleAsync(tailorUser, TailorRoleName);

            }

            var salesRepUser = await userManager.FindByEmailAsync(SalesRepUserName);
            if (salesRepUser == null)
            {
                salesRepUser = new IdentityUser
                {
                    UserName = SalesRepUserName,
                    Email = SalesRepUserName
                };
                await userManager.CreateAsync(salesRepUser, "a123456");
                await userManager.AddToRoleAsync(salesRepUser, SalesRepRoleName);

            }

        }
    }
}