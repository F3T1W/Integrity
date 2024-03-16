using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Integrity.Pages;

public partial class Index
{

    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; }

    private async Task DisplayGreetingAlert()
    {
        var authState = await authenticationState;
        var message = $"Hello {authState.User.Identity.Name}";
        await JS.InvokeVoidAsync("alert", message);
    }
}
