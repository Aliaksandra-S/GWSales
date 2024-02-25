namespace GWSales.WebApi.Models.User;

public class GetUserRolesDto
{
    public string Username { get; set; }
    public IList<string> Roles { get; set; }
}
