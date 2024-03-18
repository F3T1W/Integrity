namespace Integrity.Shared;

public partial class Header
{
    private string? currentUri;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        currentUri = NavigationManager.Uri;
    }
}
