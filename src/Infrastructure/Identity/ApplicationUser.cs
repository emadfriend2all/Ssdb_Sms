using Microsoft.AspNetCore.Identity;

namespace Showmatics.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public int? MemberId { get; set; }
    public string? ObjectId { get; set; }
    public string? ApiToken { get; set; }
}