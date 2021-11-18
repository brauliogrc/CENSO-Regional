using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CensoAPI02.Intserfaces.AuthorizePolicity
{
    public class AdminRoleAuthorize : IAuthorizationRequirement
    {
        public int adminRole { get; set; }

        public AdminRoleAuthorize(int adminRole)
        {
            this.adminRole = adminRole;
        }
    }

    public class AdminRoleAuthorizationHandler : AuthorizationHandler<AdminRoleAuthorize>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRoleAuthorize requirement)
        {
            // Validación del rol de administrador
            if ( !context.User.HasClaim( x => x.Type == "Role") )
            {
                return Task.CompletedTask;
            }

            int role = Convert.ToInt32( context.User.FindFirst(x => x.Type == "Role" ).Value );
            if ( role == requirement.adminRole )
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
