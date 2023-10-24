namespace Relaks.Utils;

public class AppOperation
{
    public delegate void RestartHandler();
    public event RestartHandler? OnRestart;
    
    public void Restart() => OnRestart?.Invoke();
}