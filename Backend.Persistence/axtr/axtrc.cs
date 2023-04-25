using Backend.Application.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Persistence.axtr
{
    public static class axtrc
    {
        public static void AddPresentation(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(Concrete.GenericRepository<>));
        }
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            
        }
    }
}
