using Backend.Application.Dtos;
using Backend.Domain.EntityModels;

namespace Backend.Application.Abstractions
{
    public interface ITokenManager
    {
        TokenDto GetAccessToken(int minutes,Employe User,IList<string> Roles);
        string getRefreshToken();
    }
}
