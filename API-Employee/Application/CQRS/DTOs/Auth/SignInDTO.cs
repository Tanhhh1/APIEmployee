namespace Application.CQRS.DTOs.Auth
{
    public class SignInDTO 
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTime Expires { get; set; }
    }
}
