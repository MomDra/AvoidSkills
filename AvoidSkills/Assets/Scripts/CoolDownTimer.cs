using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownTimer
{
    public float currTime{ get; private set; }

    public void tik(){
        if(currTime > 0) currTime -= 0.1f;
    }
    public void set(float time){
        currTime = time;
    }
    public void reset(){
        currTime = 0;
    }
}
