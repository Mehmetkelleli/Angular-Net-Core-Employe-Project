using Backend.Application.Dtos;
using Backend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Role.Query.RoleGetById
{
    public class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQueryRequest, RoleGetByIdQueryResponse>
    {
        private RoleManager<Domain.EntityModels.Role> _Role;
        private UserManager<Domain.EntityModels.Employe> _Employe;

        public RoleGetByIdQueryHandler(RoleManager<Domain.EntityModels.Role> role, UserManager<Domain.EntityModels.Employe> employe)
        {
            _Role = role;
            _Employe = employe;
        }

        public async Task<RoleGetByIdQueryResponse> Handle(RoleGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var role =await _Role.FindByIdAsync(request.Id);
            
            if (role == null)
                throw new NotFoundExc(new string[] { "role-not-found" });
            
            var roleModel = new RoleWithUserByIdDto();
            roleModel.Role = role;
            roleModel.Employes = (await _Employe.GetUsersInRoleAsync(role.Name)).AsQueryable();
            var employeList = new List<Domain.EntityModels.Employe>();
            foreach (var item in _Employe.Users)
            {
                if(!await _Employe.IsInRoleAsync(item, role.Name))
                {
                    employeList.Add(item);
                }
            }
            roleModel.NonEmployes = employeList.AsQueryable();
            var response = new RoleGetByIdQueryResponse();
            response.Role = roleModel;
            
            return response;
        }
    }
}
