using Backend.Application.Abstractions;
using Backend.Application.Abstractions.Services;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Employe.Command
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
    {
        private UserManager<Backend.Domain.EntityModels.Employe> _Employe;
        private IStorage _Storage;
        private RoleManager<Backend.Domain.EntityModels.Role> _Role;
        private IUserService _UserService;
        public UserCreateCommandHandler(UserManager<Backend.Domain.EntityModels.Employe> employe, IStorage storage, RoleManager<Backend.Domain.EntityModels.Role> role, IUserService userService)
        {
            _Employe = employe;
            _Storage = storage;
            _Role = role;
            _UserService = userService;
        }

        public async Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var resopnse = await _UserService.CreateUser(request);
            return new UserCreateCommandResponse { state= resopnse };
        }
    }
}
