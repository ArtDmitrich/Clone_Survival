using System;
public class CustomTimer
{
    public float TargetTime;
    public float CurrentTime;

    public event Action<CustomTimer> OnCallback;

    public void Tick(float deltaTime)
    {
        CurrentTime += deltaTime;

        if (CurrentTime >= TargetTime)
        {
            OnCallback?.Invoke(this);
        }
    }

    public void RemoveSubscribers()
    {
        var subs = OnCallback.GetInvocationList();

        foreach (var sub in subs)
        {
            OnCallback -= (Action<CustomTimer>)sub;
        }
    }
}
