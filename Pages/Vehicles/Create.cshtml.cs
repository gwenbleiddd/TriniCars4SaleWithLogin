using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TriniCars4SaleWithLogin.Data;
using TriniCars4SaleWithLogin.Models;

namespace TriniCars4SaleWithLogin.Pages.Vehicles
{
    public class CreateModel : DI_BasePageModel
    {
        

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, environment, userManager)
        {
           
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {



                string thumbFolder = "Images/Thumbs/";
                string galleryFolder = "Images/Gallery/";

                var data = new Vehicle()
                {

                    LicensePlate = vehicle.LicensePlate,
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Year = vehicle.Year,
                    Colour = vehicle.Colour,
                    EngineSize = vehicle.EngineSize,
                    Mileage = vehicle.Mileage,
                    Transmission = vehicle.Transmission,
                    Features = vehicle.Features,
                    AdditionalInfo = vehicle.AdditionalInfo,
                    AskingPrice = vehicle.AskingPrice,
                    ThumbUrl = "~/" + await uploadImage(thumbFolder, vehicle.ThumbFile),
                    AdditionalUrl1 = "~/" + await uploadImage(galleryFolder, vehicle.AdditionalFile1),
                    AdditionalUrl2 = "~/" + await uploadImage(galleryFolder, vehicle.AdditionalFile2),
                    ContactName = vehicle.ContactName,
                    ContactNum = vehicle.ContactNum
                };


                Context.Add(data);
                await Context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }



            return RedirectToPage("./Index");
        }

        private async Task<string> uploadImage(string folderPath, IFormFile? file)
        {
            string fileName = string.Empty;


            //string uploadFolder = Path.Combine(_environment.WebRootPath, "Images/Thumbs");
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string folder = Path.Combine(Environment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(folder, FileMode.Create));



            return folderPath;
        }
    }
}
