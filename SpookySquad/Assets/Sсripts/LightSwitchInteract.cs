using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//---------------- Скрипт для фонаря-проектора ----------------//

public class InteractableLightSwitch : MonoBehaviour
{
    public string switchName;    

    public GameObject lightobj; 
    public Renderer lightBulb;  
    public Material offlight, onlight; 

    public Animator switchAnim;
    public AudioSource lightSwitchSound; 

    public Text interactionText;

    private bool toggle = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsPlayerLookingAtSwitch())
        {
            ToggleLightSwitch();
        }
    }

    // метод для вкл/выкл света
    private void ToggleLightSwitch()
    {
        toggle = !toggle;

        if (toggle)
        {
            lightobj.SetActive(true);
            lightBulb.material = onlight;
        }
        else
        {
            lightobj.SetActive(false);
            lightBulb.material = offlight;
        }

   
        if (lightSwitchSound != null)
        {
            lightSwitchSound.Play();
        }

        if (switchAnim != null)
        {
            switchAnim.SetTrigger("press");
        }
    }

    private bool IsPlayerLookingAtSwitch()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.text = switchName; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.text = ""; 
        }
    }
}
