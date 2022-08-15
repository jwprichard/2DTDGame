using System;
using System.Timers;
using UnityEngine;

public class SimpleTimer
{
    public Timer Timer;
    public bool IsRunning;
    public delegate void Callback();
    readonly Callback callback;

    //public SimpleTimer(float time, Callback action, bool autoReset = false)
    //{
    //    callback = action;
    //    Timer = new Timer(time);
    //    Timer.Elapsed += SimpleTimer_Elapsed;
    //    Timer.AutoReset = autoReset;
    //    Timer.Enabled = true;
    //    IsRunning = true;
    //}

    public SimpleTimer(float time, bool autoReset = false)
    {
        Timer = new Timer(time);
        Timer.Elapsed += SimpleTimer_Elapsed;
        Timer.AutoReset = autoReset;
        Timer.Enabled = true;
        IsRunning = true;
    }

    private void SimpleTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        //callback();
        IsRunning = false;
    }


}