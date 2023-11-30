using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TriniCars4SaleWithLogin.Data;

namespace TriniCars4SaleWithLogin.Pages.Vehicles
{
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }
        protected IWebHostEnvironment Environment { get; }

        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager) : base()

        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
            Environment = environment;
        }
    }
}

