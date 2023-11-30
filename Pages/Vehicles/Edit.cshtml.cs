using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TriniCars4SaleWithLogin.Data;
using TriniCars4SaleWithLogin.Models;

namespace TriniCars4SaleWithLogin.Pages.Vehicles
{
    public class EditModel : DI_BasePageModel
    {
        
        public EditModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, environment, userManager)
        {
            
        }

        [BindProperty]
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
            Vehicle = vehicle;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(Vehicle vehicle,int id)
        {
            try
            {
              if(ModelState.IsValid)
                {
                   string thumbFolder = "Images/Thumbs/";

                    var data = Context.Vehicle.Where(v => v.VehicleId == vehicle.VehicleId).SingleOrDefault();

                    string ThumbUrl = string.Empty;
                    string AdditionalUrl1 = string.Empty;
                    string AdditionalUrl2 = string.Empty;

                    if(vehicle.ThumbUrl != null)
                    {
                        if(data.ThumbUrl != null)
                        {
                            string file = Path.Combine(Environment.WebRootPath, "Images/Thumbs/",data.ThumbUrl);
                            if(System.IO.File.Exists(file))
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                        ThumbUrl = "~/" + await uploadImage(thumbFolder, vehicle.ThumbFile);
                    }
                    data.LicensePlate = vehicle.LicensePlate;
                    data.Make = vehicle.Make;
                    data.Model = vehicle.Model;
                    data.Year = vehicle.Year;
                    data.Colour = vehicle.Colour;
                    data.EngineSize = vehicle.EngineSize;
                    data.Mileage = vehicle.Mileage;
                    data.Transmission = vehicle.Transmission;
                    data.Features = vehicle.Features;
                    data.AdditionalInfo = vehicle.AdditionalInfo;
                    data.AskingPrice = vehicle.AskingPrice;
                    data.ContactName = vehicle.ContactName;
                    data.ContactNum = vehicle.ContactNum;
                    if(Vehicle.ThumbUrl != null)
                    {
                        data.ThumbUrl = ThumbUrl;//not working :(
                    }
                    //Context.Attach(data).State = EntityState.Modified; 
                    Context.Vehicle.Attach(data);
                    await Context.SaveChangesAsync();
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }

            var vehicleToUpdate = await Context.Vehicle.FindAsync(id);
            if (vehicleToUpdate == null)
            {
                return NotFound();
            }
            
            //if(await TryUpdateModelAsync<Vehicle>(vehicleToUpdate
            //    ,"vehicle",
            //    v => v.LicensePlate,
            //    v => v.Make,
            //    v => v.Model,
            //    v => v.Year,
            //    v => v.Colour,
            //    v => v.EngineSize,
            //    v => v.ThumbUrl))
            //{
            //    await Context.SaveChangesAsync();
                
            //}

            
            




            

            //if (vehicle.ThumbUrl == null)
            //{
            //    Context.Entry(Vehicle).Property(m => m.ThumbUrl).IsModified = false;
            //}
            //if(vehicle.AdditionalUrl1 == null)
            //{
            //    Context.Entry(Vehicle).Property(m => m.AdditionalUrl1).IsModified = false;
            //}
            //if (vehicle.AdditionalUrl2 == null)
            //{
            //    Context.Entry(Vehicle).Property(m => m.AdditionalUrl2).IsModified = false;
            //}

            
            //try
            //{
            //    await Context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!VehicleExists(Vehicle.VehicleId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

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

        private bool VehicleExists(int id)
        {
            return Context.Vehicle.Any(e => e.VehicleId == id);
        }
    }
}
