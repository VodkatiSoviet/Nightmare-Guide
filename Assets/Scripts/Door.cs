using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorState; //true면 닫힌상태 false면 열린상태
    public Animator doorAnimator;
    public BoxCollider boxcollider;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider>();
    }

    public void Select_Door()
    {
        if(doorState)
        {
            Debug.Log("OpenDoor");
            doorAnimator.SetBool("Open",true);
            doorAnimator.SetBool("Close", false);
            doorState = false;
        }
        else if (!doorState)
        {
            Debug.Log("CloseDoor");
            doorAnimator.SetBool("Open", false);
            doorAnimator.SetBool("Close", true);
            doorState = true;
        }
    }
    public void istrigger_on()
    {
        boxcollider.isTrigger = true;
    }
    public void istrigger_off()
    {
        boxcollider.isTrigger = false;
    }

}
