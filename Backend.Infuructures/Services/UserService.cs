using Backend.Application.Abstractions;
using Backend.Application.Abstractions.Services;
using Backend.Application.Exceptions;
using Backend.Application.Features.Employe.Command;
using Backend.Domain.EntityModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Backend.Infuructures.Services
{
    public class UserService : IUserService
    {
        private UserManager<Employe> _Employe;
        private IStorage _Storage;
        private RoleManager<Role> _Role;
        public UserService(UserManager<Employe> employe, IStorage storage, RoleManager<Role> role)
        {
            _Employe = employe;
            _Storage = storage;
            _Role = role;
        }

        public async Task<bool> CreateUser(UserCreateCommandRequest model)
        {
            var employe = new Backend.Domain.EntityModels.Employe()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                LastName = model.LastName,
                Age = model.Age,
                Email = model.EMail,
                ImagePath = _Storage.ReName(model.Picture.FileName),
                UserName = model.UserName
            };
            if (await _Employe.FindByEmailAsync(model.EMail) != null)
            {
                throw new CustomException(new string[] { "email-dublicate" });
            }
            if (await _Storage.SendFile(model.Picture, ".jpg", "users"))
            {
                var result = await _Employe.CreateAsync(employe, model.Password);
                if (result.Succeeded)
                {
                    if (_Employe.Users.Count() == 1)
                    {
                        if (await _Role.FindByNameAsync("Admin") == null)
                        {
                            await _Role.CreateAsync(new Backend.Domain.EntityModels.Role { Id = Guid.NewGuid().ToString(), Name = "Admin", Wage = 0, OvertimePay = 0 });
                            await _Employe.AddToRoleAsync(employe, "Admin");
                        }
                        else
                        {
                            await _Employe.AddToRoleAsync(employe, "Admin");
                        }
                    }
                }
                else
                {
                    try
                    {
                        _Storage.Delete(employe.ImagePath, "users");
                    }
                    catch (Exception)
                    {

                    }
                    string[] list = new string[] { };
                    foreach (var item in result.Errors)
                    {
                        list.Append(item.Description);
                    }
                    throw new CustomException(list);
                }
            }
            return true;
        }

        public async Task<bool> UpdateRefreshToken(string UserId,string AccessToken, DateTime TokenDate, int minutes)
        {
            var user = await _Employe.FindByIdAsync(UserId);
            user.RefreshToken = AccessToken ;
            user.RefreshTokenEndDate = TokenDate.AddMinutes(minutes);
            var result = await _Employe.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
