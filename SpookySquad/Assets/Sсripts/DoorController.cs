using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public string openTriggerName = "open";
    public string closeTriggerName = "close";

    private bool isOpen = false;

    private void Start()
    {
        doorAnimator.SetBool(openTriggerName, false);
        doorAnimator.SetBool(closeTriggerName, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        doorAnimator.SetBool(openTriggerName, true);
        doorAnimator.SetBool(closeTriggerName, false);
        isOpen = true;
    }

    private void CloseDoor()
    {
        doorAnimator.SetBool(openTriggerName, false);
        doorAnimator.SetBool(closeTriggerName, true);
        isOpen = false;
    }
}

*/