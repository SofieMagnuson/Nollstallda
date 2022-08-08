using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    public Animator animController;
    public float timer;

    private void Start()
    {
        timer = 2f;
    }

    public void animTimer()
    {
        animController.SetBool("animClock", true);

    }
    public void animTimerOff()
    {
            animController.SetBool("animClock", false);
    }

    public void animShower()
    {
        animController.SetBool("ShowerAnim", true);
    }
    public void animShowerDone()
    {
        animController.SetBool("ShowerAnim", false);
    }
}
