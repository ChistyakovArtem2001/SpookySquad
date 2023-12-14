using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//---------------- Скрипт отвечающий за статусы игрока ----------------//


public class PlayerState : MonoBehaviour
{
    // --- Player Health --- //
    public float currentHealth; // Текущее здоровье игрока
    public float maxHealth;     // Максимальное здоровье игрока

    // --- Player Fear --- //
    public float currentFear; // Текущий уровень рассудка игрока
    public float maxFear;     // Максимальный уровень рассудка игрока

    public bool isFearActive; // Флаг, указывающий, активен ли страх (мб добавить механику сейв-зон)

    public static PlayerState Instance { get; set; } // Ссылка на экземпляр класса PlayerState

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        currentHealth = maxHealth;     // Устанавливаем текущее здоровье игрока равным максимальному
        currentFear = maxFear;         // Устанавливаем текущий уровень рассудка игрока равным максимальному

        StartCoroutine(decreseFear()); // Запускаем корутину для уменьшения уровня страха
    }

    IEnumerator decreseFear()
    {
        while (true)
        {
            currentFear -= 1;                    // Уменьшаем уровень страха на 1
            yield return new WaitForSeconds(20); // Ждем 20 секунд перед следующим уменьшением
        }
    }


    void Update()
    {
        // тестовый тест
        // if(Input.GetKeyDown(KeyCode.N))
        // {
        //    currentHealth -= 10;
        // }
    }


    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }


    public void setFear(float newFear)
    {
        currentFear = newFear;
    }
}
