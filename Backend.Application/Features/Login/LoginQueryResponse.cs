namespace Backend.Application.Features.Login
{
    public class LoginQueryResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
