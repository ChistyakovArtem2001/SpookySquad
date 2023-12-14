using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------- Скрипт для мигающих ламп ----------------//

public class Flicker_Light : MonoBehaviour
{
    public Light lightobj;
    public float minWaitTime = 0.1f;
    public float maxWaitTime = 0.5f;

    //private bool isFlickering = false;

    private void Start()
    {
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            
            lightobj.enabled = !lightobj.enabled;
            
            yield return new WaitForSeconds(waitTime);
        }
    }
}
