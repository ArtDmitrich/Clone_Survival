using System;
using static UnityEngine.Application;

public class CustomStopwatch
{
    public Action<int, int> TimeChanged;

    private float _currentTime;
    private float _elapsedDeltaTime;

    public void Tick(float deltaTime)
    {
        _currentTime += deltaTime;

        _elapsedDeltaTime += deltaTime;
        
        if(_elapsedDeltaTime > 1.0f )
        {
            _elapsedDeltaTime = 0.0f;

            var time = TimeSpan.FromSeconds(_currentTime);
            var minutes = time.Minutes;
            var seconds = time.Seconds;

            TimeChanged?.Invoke(minutes, seconds);
        }
    }
    public void RemoveSubscribers()
    {
        var subs = TimeChanged.GetInvocationList();

        foreach (var sub in subs)
        {
            TimeChanged -= (Action<int, int>)sub;
        }
    }
}
