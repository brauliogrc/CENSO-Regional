using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CensoAPI02.Intserfaces.AuthorizePolicity
{

    public class StaffRhRoleAuthorization : IAuthorizationRequirement
    {
        public int adminRole { get; set; }

        public int surhRole { get; set; }

        public int stahhRHRole { get; set; }

        public StaffRhRoleAuthorization(int adminRole, int surhRole, int stahhRHRole)
        {
            this.adminRole = adminRole;
            this.surhRole = surhRole;
            this.stahhRHRole = stahhRHRole;
        }
    }

    public class StaffRHRoleAuthorizationHandler : AuthorizationHandler<StaffRhRoleAuthorization>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StaffRhRoleAuthorization requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "Role"))
            {
                return Task.CompletedTask;
            }

            int role = Convert.ToInt32(context.User.FindFirst(x => x.Type == "Role").Value);
            if(role == requirement.adminRole || role == requirement.surhRole || role == requirement.stahhRHRole )
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
