using Backend.Application.Dtos;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Role.Command.RoleUpdate
{
    
    public class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommandRequest, RoleUpdateCommandResponse>
    {
        private RoleManager<Domain.EntityModels.Role> _Role;
        private UserManager<Domain.EntityModels.Employe> _Employe;

        public RoleUpdateCommandHandler(RoleManager<Domain.EntityModels.Role> role, UserManager<Domain.EntityModels.Employe> employe)
        {
            _Role = role;
            _Employe = employe;
        }

        public async Task<RoleUpdateCommandResponse> Handle(RoleUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var role =await _Role.FindByIdAsync(request.Id);
          
            if(role == null)
            {
                throw new NotFoundExc(new string[] { "role-not-found" });
            }
            
            role.OvertimePay = request.OvertimePay;
            role.Wage = request.Wage;
            role.Name = request.Name;

            var result = await _Role.UpdateAsync(role);
            if (result.Succeeded)
            {

                foreach (var item in request.NonUserId)
                {
                    var user = await _Employe.FindByIdAsync(item);
                    if (user == null)
                        throw new NotFoundExc(new string[] { "user-not-found" });
                    await _Employe.RemoveFromRoleAsync(user, role.Name);
                }

                foreach (var item in request.UserId)
                {
                    var user = await _Employe.FindByIdAsync(item);
                    if (user == null)
                        throw new NotFoundExc(new string[] { "user-not-found" });
                    await _Employe.AddToRoleAsync(user, role.Name);
                }
                var response = new RoleUpdateCommandResponse();

                var roleModel = new RoleWithUserDto();
                roleModel.Role = role;
                roleModel.Employes = (await _Employe.GetUsersInRoleAsync(role.Name)).AsQueryable();

                response.Role = roleModel;
                
                return response;
            }
            
            string[] errors = new string[] { };
            
            foreach (var item in result.Errors)
            {
                errors.Append(item.Description);
            }
            throw new CustomException(errors);
        }
    }
}
