namespace GWSales.WebApi.Models.User;

public class GetTokenDto
{
    public string Username { get; set; }
    public IList<string> Roles { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
