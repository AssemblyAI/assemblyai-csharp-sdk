namespace BlazorSample;

public class AppState 
{
    public string TopRowTitle { get; set; }
    public event Action? StateHasChanged;
    public void NotifyStateChanged() => StateHasChanged?.Invoke();
}