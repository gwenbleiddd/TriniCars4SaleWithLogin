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
    public class IndexModel : DI_BasePageModel
    {
        //private readonly TriniCars4SaleWithLogin.Data.ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, environment, userManager)
        {
            //_context = context;
        }

        public IList<Vehicle> Vehicle { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Vehicle = await Context.Vehicle.ToListAsync();
        }
    }
}
