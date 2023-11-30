using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using TriniCars4SaleWithLogin.Models;

namespace TriniCars4SaleWithLogin.Authorization
{
    
    
        public class VehicleAdministratorsAuthorizationHandler
                   : AuthorizationHandler<OperationAuthorizationRequirement, Vehicle>
        {
            protected override Task HandleRequirementAsync(
                                        AuthorizationHandlerContext context,
                                        OperationAuthorizationRequirement requirement,
                                        Vehicle resource)
            {
                if (context.User == null)
                {
                    return Task.CompletedTask;
                }

                // Administrators can do anything.
                if (context.User.IsInRole(Constants.VehicleAdministratorsRole))
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
        }
    
}
