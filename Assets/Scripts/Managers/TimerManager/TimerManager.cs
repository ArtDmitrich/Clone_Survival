using System;
using System.Collections.Generic;
using UnityEngine;

public class TimersManager : MonoBehaviour
{
    private readonly List<CustomTimer> _timers = new List<CustomTimer>();
    private readonly List<CustomStopwatch> _stopwatches = new List<CustomStopwatch>();

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

    public void RemoveAllTimers()
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            _timers[i].RemoveSubscribers();
        }

        _timers.Clear();
    }

    public void SetStopwatch(Action<int, int> CallBack)
    {
        var stopwatch = new CustomStopwatch();
        stopwatch.TimeChanged += CallBack;

        _stopwatches.Add(stopwatch);
    }

    public void RemoveAllStopwatches()
    {
        for (int i = 0; i < _stopwatches.Count; i++)
        {
            _stopwatches[i].RemoveSubscribers();
        }

        _stopwatches.Clear();
    }

    private void Update()
    {
        if (_timers.Count != 0)
        {
            for (int i = 0; i < _timers.Count; i++)
            {
                _timers[i].Tick(Time.deltaTime);
            }
        }

        if (_stopwatches.Count != 0)
        {
            for (int i = 0; i < _stopwatches.Count; i++)
            {
                _stopwatches[i].Tick(Time.deltaTime);
            }
        }
    }
}
