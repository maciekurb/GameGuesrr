namespace GameGuessr.Client.Application;

public class LoaderService
{
    public event Action<bool> LoaderState;

    public void SetLoaderState(bool state)
    {
        LoaderState?.Invoke(state);
    }
}
