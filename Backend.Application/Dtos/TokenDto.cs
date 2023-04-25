namespace Backend.Application.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresDate { get; set; }
    }
}
