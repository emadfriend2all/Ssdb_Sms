using Showmatics.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Showmatics.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = FSHPermission.NameFor(action, resource);
}