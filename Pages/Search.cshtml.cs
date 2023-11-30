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
    public class SearchModel : DI_BasePageModel
    {
        

        public SearchModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, environment, userManager)
        {
            
        }

        public IList<Vehicle> Vehicle { get; set; } = default!;
        
        public async Task OnGetAsync(string SearchID, string SearchMake,
            string SearchYear, string SearchLicense, string SearchTransmission, string SearchSize,
            string SearchModel, string SearchPrice)
        {
            var vehicle = from v in  Context.Vehicle
                          select v;


            if (!String.IsNullOrEmpty(SearchID))
            {
                int ID = Int32.Parse(SearchID);
                vehicle = vehicle.Where(v => v.VehicleId == (ID));
            }
            else if (!String.IsNullOrEmpty(SearchMake))
            {

                vehicle = vehicle.Where(v => v.Make.ToLower().Contains(SearchMake));
            }
            else if (!String.IsNullOrEmpty(SearchYear))
            {
                int ID = Int32.Parse(SearchYear);
                vehicle = vehicle.Where(v => v.Year == (ID));
            }

            else if (!String.IsNullOrEmpty(SearchLicense))
            {
                vehicle = vehicle.Where(v => v.LicensePlate.ToLower().Contains(SearchLicense));
            }
            else if (!String.IsNullOrEmpty(SearchTransmission))
            {
                if (SearchTransmission == "Automatic")
                {
                    vehicle = vehicle.Where(v => v.Transmission.Contains("Automatic"));
                }
                else if (SearchTransmission == "Manual")
                {
                    vehicle = vehicle.Where(v => v.Transmission.Contains("Manual"));
                }

            }
            else if (!String.IsNullOrEmpty(SearchModel))
            {
                vehicle = vehicle.Where(v => v.Model.ToLower().Contains(SearchModel));
            }
            else if (!String.IsNullOrEmpty(SearchSize))
            {
                vehicle = vehicle.Where(v => v.EngineSize.Contains(SearchSize));
            }
            else if (!String.IsNullOrEmpty(SearchPrice))
            {
                if (SearchPrice == "Minimum")
                {
                    vehicle = vehicle.Where(v => v.AskingPrice < 100);
                }
                else if (SearchPrice == "Maximum")
                {
                    vehicle = vehicle.Where(v => v.AskingPrice > 200);
                }

            }


            //return Page();

            Vehicle = await vehicle.ToListAsync();
        }
    }
}
