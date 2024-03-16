using Integrity.Authentication;

namespace Integrity.Shared;

public partial class Header
{
    private string currentUri;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        currentUri = NavigationManager.Uri;
    }

    private async Task Logout()
    {
        var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
        await customAuthStateProvider.UpdateAuthenticationState(null);
        NavigationManager.NavigateTo("/", true);
    }
}
