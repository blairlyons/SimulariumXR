using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StopMotionAnimator : MonoBehaviour
{
    public int currentFrame = -1;
    public int frameRate;
    [SerializeField]
    protected bool playing;
    protected int animationLength;
    protected float lastTime = -10f;

    protected void Animate ()
    {
        if (playing && Time.time - lastTime >= 1f / frameRate)
        {
            int _frame = currentFrame >= animationLength - 1 ? 0 : currentFrame + 1;
            RenderFrame(_frame);
            lastTime = Time.time;
        }
    }
    
    public void Play ()
    {
        playing = true;
        lastTime = 0;
    }

    public void Pause ()
    {
        playing = false;
    }

    public virtual void RenderFrame (int _frame)
    {
        currentFrame = _frame;
    }
}
