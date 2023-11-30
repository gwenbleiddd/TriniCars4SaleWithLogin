using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TriniCars4SaleWithLogin.Data;
using TriniCars4SaleWithLogin.Models;
using TriniCars4SaleWithLogin.Pages.Vehicles;

namespace TriniCars4SaleWithLogin.Pages
{
    [AllowAnonymous]
    public class DetailsModel : DI_BasePageModel
    {
        

        public DetailsModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, environment, userManager)
        {
            
        }

        public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await Context.Vehicle.FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                Vehicle = vehicle;
            }
            return Page();
        }
    }
}
