using System.Security.Claims;
using Showmatics.Application.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Showmatics.Shared.Authorization;

namespace Showmatics.Infrastructure.Auth.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService;

    public PermissionAuthorizationHandler(IUserService userService) =>
        _userService = userService;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User?.GetUserId() is { } userId &&
            await _userService.HasPermissionAsync(userId, requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}

//internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
//{
//    private readonly IServiceProvider _serviceProvider;
//    public PermissionAuthorizationHandler(IServiceProvider serviceProvider)
//    {
//        _serviceProvider = serviceProvider;
//    }

//    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
//    {
//        var userService = _serviceProvider.GetRequiredService<IUserService>();

//        if (context.User == null)
//        {
//            await Task.CompletedTask;
//        }


//        if (context.User.Claims.Any())
//        {
//            var appToken = context.User.Claims.FirstOrDefault(c => c.Type == FSHClaims.ApiToken).Value;
//            var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

//            if (!await userService.IsValidToken(appToken, userId) && !string.IsNullOrEmpty(appToken))
//            {
//                context.Fail();
//                await Task.CompletedTask;
//            }

//            var permissions = context.User.Claims.Where(x => x.Type == "Permission" &&
//                                           x.Value == requirement.Permission);
//            if (permissions.Any())
//            {
//                context.Succeed(requirement);
//                await Task.CompletedTask;
//            }
//        }

//    }

//}