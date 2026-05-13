using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TriniCars4SaleWithLogin.Models;
using TriniCars4SaleWithLogin.Authorization;

namespace TriniCars4SaleWithLogin.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string Pw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, Pw, "admin@contoso.com", "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.VehicleAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, Pw, "manager@contoso.com", "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.VehicleManagersRole);

                SeedDB(context, adminID);
            }
        }
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName, string email)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
           
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    Email = email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }
        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Vehicle.Any())
            {
                return;   // DB has been seeded
            }
            context.Vehicle.AddRange(
                    new Vehicle
                    {

                        LicensePlate = "PBZ",
                        Make = "Toyota",
                        Model = "Corolla",
                        Year = 1985,
                        Colour = "Blue",
                        EngineSize = "1500 cc",
                        Mileage = "21,000 km",
                        Transmission = "Automatic, Tiptronic",
                        Features = "Air Condition, Power Windows, Power Locks, Power Mirrors",
                        AdditionalInfo = "Car like brand new.",
                        AskingPrice = 190.000M,
                        ContactName = "Jerry Garcia",
                        ContactNum = "778-8845",
                        ThumbUrl = "/Images/Thumbs/corolla.jpg",
                        Status = VehicleStatus.Approved,
                        OwnerID = adminID
                    },
                     new Vehicle
                     {

                         LicensePlate = "PCD",
                         Make = "Nissan",
                         Model = "Tiida",
                         Year = 2009,
                         Colour = "Purple",
                         EngineSize = "1500 cc",
                         Mileage = "160,000 km",
                         Transmission = "Automatic",
                         Features = "Power Windows, Power Mirrors, Tint",
                         AdditionalInfo = "Recently painted, air condition very cold.",
                         AskingPrice = 35.500M,
                         ContactName = "Val Kilmer",
                         ContactNum = "745-1845",
                         ThumbUrl = "/Images/Thumbs/tiida.jpg",
                         AdditionalUrl1 = "/Images/Gallery/c1d0a0e8-3829-491f-8cdc-612b8a72091c_audi1.jpg",
                         AdditionalUrl2 = "/Images/Gallery/7ea16608-8676-4f96-8343-3c33ebb09498_audi2.jpg",
                         Status = VehicleStatus.Approved,
                         OwnerID = adminID
                     },
                      new Vehicle
                      {

                          LicensePlate = "PAC",
                          Make = "Suzuki",
                          Model = "Jimney",
                          Year = 1985,
                          Colour = "Yellow",
                          EngineSize = "1300 cc",
                          Mileage = "",
                          Transmission = "Manual, 5 Forward",
                          Features = "Air Condition, Power Steering, 4 Wheel Drive, Fog Lamps",
                          AdditionalInfo = "Very good condition.",
                          AskingPrice = 10.020M,
                          ContactName = "Monkey D Luffy",
                          ContactNum = "758-4545",
                          ThumbUrl = "/Images/Thumbs/jimney.jpg",
                          Status = VehicleStatus.Approved,
                          OwnerID = adminID
                      },
                      new Vehicle
                      {

                          LicensePlate = "PEC",
                          Make = "Honda",
                          Model = "Vezel",
                          Year = 2022,
                          Colour = "Red",
                          EngineSize = "1500 cc",
                          Mileage = "",
                          Transmission = "Automatic",
                          Features = "Reverse Sensors, Reverse Camera, Fabric Interior,",
                          AdditionalInfo = "18 inch Honda rims. LED day time running lights. " +
                          "Blind spot indicates. Anti collision sonar. " +
                          "Lane keeping assist. Automatic tail gate opening. " +
                          "All wheel drive.",
                          AskingPrice = 218.200M,
                          ContactName = "Trafalga Law",
                          ContactNum = "339-8255",
                          ThumbUrl = "/Images/Thumbs/vezel1.jpg",
                          Status = VehicleStatus.Approved,
                          OwnerID = adminID
                      }
                );
            context.SaveChanges();
        }
    }
}
