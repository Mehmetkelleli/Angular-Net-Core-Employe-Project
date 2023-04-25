using Serilog.Context;

namespace Backend.Service.Extensions
{
    public static class UserLogExtensions
    {
        public static async Task AddUserLog(this WebApplication app,HttpContext Context,Func<Task> Next) 
        {
            
            var ipAdress = Context.Connection.RemoteIpAddress;
            var userName = Context.User?.Identity?.IsAuthenticated == true ? Context.User.Identity.Name : ipAdress?.ToString();
            LogContext.PushProperty("user_name", userName + " " + ipAdress);
            
            await Next();
        }
    }
}
