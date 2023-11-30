using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TriniCars4SaleWithLogin.Authorization;
using TriniCars4SaleWithLogin.Data;
using TriniCars4SaleWithLogin.Models;

namespace TriniCars4SaleWithLogin.Pages.Vehicles
{
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
            Vehicle? _vehicle = await Context.Vehicle.FirstOrDefaultAsync(m => m.VehicleId == id);

            if (id == null)
            {
                return NotFound();
            }

            Vehicle = _vehicle;

            var isAuthorized = User.IsInRole(Constants.VehicleManagersRole) ||
                               User.IsInRole(Constants.VehicleAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Vehicle.OwnerID
                && Vehicle.Status != VehicleStatus.Approved)
            {
                return Forbid();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, VehicleStatus status)
        {
            var contact = await Context.Vehicle.FirstOrDefaultAsync(
                                                      m => m.VehicleId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var contactOperation = (status == VehicleStatus.Approved)
                                                       ? VehicleOperations.Approve
                                                       : VehicleOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, contact,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            contact.Status = status;
            Context.Vehicle.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
