using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnLose = new UnityEvent();
    public static UnityEvent OnWin = new UnityEvent();

    public static void Lose()
    {
        OnLose?.Invoke();
    }

    public static void Win()
    {
        OnWin?.Invoke();
    }
}
