using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownTimer
{
    public float currTime { get; private set; }
    public bool IsReady { get; private set; }

    public CoolDownTimer()
    {
        currTime = 0f;
        IsReady = true;
    }

    public void tik()
    {
        if (currTime > 0) currTime -= 0.1f;
    }
    public void set(float time)
    {
        IsReady = false;
        currTime = time;
    }
    public void reset()
    {
        IsReady = true;
        currTime = 0;
    }


}
