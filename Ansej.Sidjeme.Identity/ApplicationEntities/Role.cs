using Ansej.Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Ansej.Sidjeme.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Role : IdentityRole, IEntity<string>
    {

    }
}
