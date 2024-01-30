using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Entities.Models;

public class CustomRoleAuthorizationHandler : AuthorizationHandler<CustomRoleRequirement>
{
    private readonly ApplicationDbContext _dbContext;

    public CustomRoleAuthorizationHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleRequirement requirement)
    {
        if (context.User.HasClaim(ClaimTypes.Role, requirement.Role))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }


    // protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleRequirement requirement)
    // {
    //     var user = _dbContext.applicationUsers.FirstOrDefault(u => u.Email == context.User.Identity.Name);

    //     if (user != null && user.Role == requirement.Role)
    //     {
    //         context.Succeed(requirement);
    //     }
    //     else
    //     {
    //         context.Fail();
    //     }
    // }
}
