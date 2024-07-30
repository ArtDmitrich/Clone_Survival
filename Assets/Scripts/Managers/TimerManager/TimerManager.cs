using System;
using System.Collections.Generic;
using UnityEngine;

public class TimersManager : Singleton<TimersManager>
{
    private readonly List<CustomTimer> _timers = new List<CustomTimer>();

    public void SetTimer(float time, Action CallBack)
    {
        var timer = new CustomTimer();
        timer.TargetTime = time;
        timer.CurrentTime = 0.0f;
        timer.OnCallback += (CustomTimer timer) =>
        {
            timer.RemoveSubscribers();
            _timers.Remove(timer);
            CallBack();
        };

        _timers.Add(timer);
    }

    public void RemoveAllTmers()
    {
        _timers.Clear();
    }

    private void Update()
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            _timers[i].Tick(Time.deltaTime);
        }
    }
}
