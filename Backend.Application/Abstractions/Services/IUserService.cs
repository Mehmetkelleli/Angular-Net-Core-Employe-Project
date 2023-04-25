using Backend.Application.Features.Employe.Command;
using Backend.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateCommandRequest model);
        Task<bool> UpdateRefreshToken(string UserId,string AccessToken,DateTime TokenDate,int minutes);
    }
}
