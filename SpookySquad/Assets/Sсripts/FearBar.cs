using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//---------------- Скрипт для рассудка ----------------//

public class FearBar : MonoBehaviour
{
    private Slider slider;
    public Text fearCounter;

    public GameObject playerState;

    private float currentFear, maxFear;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {

        currentFear = playerState.GetComponent<PlayerState>().currentFear;
        maxFear = playerState.GetComponent<PlayerState>().maxFear;

        float fillValue = currentFear / maxFear;
        slider.value = fillValue;

        fearCounter.text = currentFear + "%";
    }
}
