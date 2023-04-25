using Backend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.Application.Features.Role.Command.RoleAdd
{
    public class RoleAddCommandHandler : IRequestHandler<RoleAddCommandRequest, RoleAddCommandResponse>
    {
        private RoleManager<Domain.EntityModels.Role> _Role;

        public RoleAddCommandHandler(RoleManager<Domain.EntityModels.Role> role)
        {
            _Role = role;
        }

        async Task<RoleAddCommandResponse> IRequestHandler<RoleAddCommandRequest, RoleAddCommandResponse>.Handle(RoleAddCommandRequest request, CancellationToken cancellationToken)
        {
            var role = new Domain.EntityModels.Role();
            role.Id = Guid.NewGuid().ToString();
            role.Name = request.Name;
            role.Wage = request.Wage;
            role.OvertimePay = request.OvertimePay;

            var result = await _Role.CreateAsync(role);

            if (!result.Succeeded)
            {
                string[] errors = new string[] { };
                foreach (var item in result.Errors)
                {
                    errors.Append(item.Description);
                }
                throw new CustomException(errors);
            }
            var response = new RoleAddCommandResponse();
            response.Role = role;
            return response;
        }
    }
}
