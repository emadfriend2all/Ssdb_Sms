﻿using Showmatics.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Showmatics.Blazor.Client.Infrastructure.Auth;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = FSHPermission.NameFor(action, resource);
}