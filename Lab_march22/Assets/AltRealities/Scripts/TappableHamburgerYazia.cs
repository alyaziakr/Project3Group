using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappableHamburgerYazia : MonoBehaviour, ITappableObject
{
    public void HandleTouchStart(TapEventData data)
    {
        //Debug.Log("touch hamburger");
    }

    public void HandleTouchEnd(TapEventData data)
    {
        //Debug.Log("stop touching hamburger");
    }

    public void HandleTap(TapEventData data)
    {
        //Debug.Log("tap on hamburger");
        LeanTween.moveY(gameObject, .5f, .5f).setLoopPingPong(1).setEaseInBounce();
    }
}