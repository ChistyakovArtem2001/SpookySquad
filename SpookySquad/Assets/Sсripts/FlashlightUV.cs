using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------- Скрипт для фонарика ----------------//

public class FlashlightUV : MonoBehaviour
{
    public GameObject flashlight; 
    private bool isFlashlightOn = false; 

    public InventorySystem inventorySystem;

    private void Start()
    {
        isFlashlightOn = inventorySystem.CheckForItem("Flashlight");
        flashlight.SetActive(isFlashlightOn);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {

        if (inventorySystem.CheckForItem("Flashlight"))
        {
            isFlashlightOn = !isFlashlightOn; 
            flashlight.SetActive(isFlashlightOn); 
        }
    }
}
