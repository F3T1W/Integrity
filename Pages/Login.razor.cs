using Integrity.Authentication;
using Microsoft.JSInterop;

namespace Integrity.Pages;

public partial class Login
{
    private class Model
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    private Model model = new ();

    private async Task Authenticate()
    {
        var userAccount = UserAccountService.GetByUserName(model.UserName);
        if (userAccount == null || userAccount.Password != model.Password)
        {
            await JS.InvokeVoidAsync("alert", "Invalid Username or Password");
            return;
        }

        var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
        await customAuthStateProvider.UpdateAuthenticationState(new UserSession
        {
            UserName = userAccount.UserName,
            Role = userAccount.Role
        });
        NavigationManager.NavigateTo("/", true);
    }
}
