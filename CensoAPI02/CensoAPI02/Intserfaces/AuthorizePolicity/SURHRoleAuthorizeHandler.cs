using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CensoAPI02.Intserfaces.AuthorizePolicity
{

    public class SUHRRoleAuthorize : IAuthorizationRequirement
    {
        public int adminRole { get; set; }

        public int surhRole { get; set; }

        public SUHRRoleAuthorize(int adminRole, int surhRole)
        {
            this.adminRole = adminRole;
            this.surhRole = surhRole;
        }
    }

    public class SURHRoleAuthorizeHandler : AuthorizationHandler<SUHRRoleAuthorize>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SUHRRoleAuthorize requirement)
        {
            if ( !context.User.HasClaim( x => x.Type == "Role") )
            {
                return Task.CompletedTask;
            }

            int role = Convert.ToInt32( context.User.FindFirst(x => x.Type == "Role").Value );
            if ( role == requirement.adminRole || role == requirement.surhRole )
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
