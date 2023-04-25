using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Application.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplication(this IServiceCollection Services)
        {
            Services.AddMediatR(typeof(ApplicationServices));
            
            //dİSABLE AutoValidations
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
