using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    public UnityEvent OnTimesUp = new UnityEvent();

    public float _passedTime;
    private float _time;
    public float currentTime
    {
        get { return _time - _passedTime; }
        private set { }

    }


    public Timer(float time)
    {
        _passedTime = 0;
        _time = time;
    }


    public void Update()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime>=_time)
        {
            OnTimesUp?.Invoke();
        }
    }

    public void Reset()
    {
        _passedTime = 0;
    }

    public float GetTime()
    {
        return _time - _passedTime;
    }
}
