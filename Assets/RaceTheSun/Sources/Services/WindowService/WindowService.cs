public class WindowService : IWindowService
{
    private readonly IUiFactory _uiFactory;

    public WindowService(IUiFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void Open(WindowId windowId)
    {
        switch (windowId)
        {
            case WindowId.Unknown:
                break;
            case WindowId.Shop:
                _uiFactory.CreateShop();
                break;
        }
    }
}