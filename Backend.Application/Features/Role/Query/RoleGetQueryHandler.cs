using Backend.Application.Dtos;
using Google.Apis.Logging;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Backend.Application.Features.Role.Query
{
    public class RoleGetQueryHandler : IRequestHandler<RoleGetQueryRequest, RoleGetRequestResponse>
    {
        private RoleManager<Backend.Domain.EntityModels.Role> _Role;
        private UserManager<Backend.Domain.EntityModels.Employe> _Employe;
        private readonly ILogger<RoleGetQueryHandler> _Logger;
        public RoleGetQueryHandler(RoleManager<Domain.EntityModels.Role> role, UserManager<Domain.EntityModels.Employe> employe, ILogger<RoleGetQueryHandler> logger)
        {
            _Role = role;
            _Employe = employe;
            _Logger = logger;
        }

        public async Task<RoleGetRequestResponse> Handle(RoleGetQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new RoleGetRequestResponse();
            var roleList = new List<RoleWithUserDto>();
            foreach (var item in _Role.Roles)
            {
                var roleModel = new RoleWithUserDto();
                roleModel.Role = item;
                roleModel.Employes = (await _Employe.GetUsersInRoleAsync(item.Name)).AsQueryable();
                roleList.Add(roleModel);
            }
            _Logger.LogInformation("Get Role List");
            response.Roles = roleList.AsQueryable();
            return response;
        }
    }
}
