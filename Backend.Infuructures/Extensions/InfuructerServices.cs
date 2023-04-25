using Backend.Application.Abstractions;
using Backend.Application.Abstractions.Services;
using Backend.Infuructures.Concrete;
using Backend.Infuructures.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infuructures.Extensions
{
    public static class InfuructerServices
    {
        public static void AddInfuructers<T>(this IServiceCollection Services) where T : class,IStorage 
        {
            Services.AddScoped<IStorage,T>();
            Services.AddScoped<ITokenManager, TokenManager>();
            Services.AddScoped<IUserService, UserService>();
        }
    }
}
