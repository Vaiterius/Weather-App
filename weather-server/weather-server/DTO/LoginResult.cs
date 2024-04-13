namespace weather_server.DTO
{
    public class LoginResult
    {
        public Boolean Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
