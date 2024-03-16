namespace Integrity.Authentication;

public class UserAccountService
{
    private List<UserAccount> _users;

    public UserAccountService()
    {
        _users = new List<UserAccount>
        {
            new () { UserName = "admin", Password = "admin", Role = "Administrator" },
            new () { UserName = "user", Password = "user", Role = "User" }
        };
    }

    public UserAccount? GetByUserName(string userName)
    {
        return _users.FirstOrDefault(x => x.UserName == userName);
    }
}
