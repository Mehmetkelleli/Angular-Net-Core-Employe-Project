using Backend.Application.Dtos;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Role.Command.RoleUpdate
{
    
    public class RoleRemoveCommandHandler : IRequestHandler<RoleRemoveCommandRequest, RoleRemoveCommandResponse>
    {
        private RoleManager<Domain.EntityModels.Role> _Role;
        private UserManager<Domain.EntityModels.Employe> _Employe;

        public RoleRemoveCommandHandler(RoleManager<Domain.EntityModels.Role> role, UserManager<Domain.EntityModels.Employe> employe)
        {
            _Role = role;
            _Employe = employe;
        }

        public async Task<RoleRemoveCommandResponse> Handle(RoleRemoveCommandRequest request, CancellationToken cancellationToken)
        {
            var role =await _Role.FindByIdAsync(request.Id);
          
            if(role == null)
            {
                throw new NotFoundExc(new string[] { "role-not-found" });
            }

            var result = await _Role.DeleteAsync(role);
            if (result.Succeeded)
            {
                var response = new RoleRemoveCommandResponse();
                response.Role = role;
                return response;
            }    

            throw new CustomException(new string[] {"role-not-delete"});
        }
    }
}
