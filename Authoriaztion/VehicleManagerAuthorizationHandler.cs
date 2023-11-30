using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using TriniCars4SaleWithLogin.Models;

namespace TriniCars4SaleWithLogin.Authorization
{
    public class VehicleManagerAuthorizationHandler :
         AuthorizationHandler<OperationAuthorizationRequirement, Vehicle>
    {
        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Vehicle resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if (requirement.Name != Constants.ApproveOperationName &&
                requirement.Name != Constants.RejectOperationName)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.VehicleManagersRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
