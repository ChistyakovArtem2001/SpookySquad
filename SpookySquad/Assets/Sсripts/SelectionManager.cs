using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//---------------- Скрипт отвечающий за статусы игрока ----------------//


public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; } // Ссылка на экземпляр класса SelectionManager

    public bool onTarget;                                 // Флаг, указывающий, находится ли игрок на объекте
    public GameObject selectedObject;                     // Выбранный объект

    public GameObject InteractionInfo;                    // Объект для отображения информации о взаимодействии
    TextMeshProUGUI interaction_text;                     // Текст для отображения информации о взаимодействии

    public bool handIsVisible;                            // Флаг, указывающий, виден ли курсор

    private void Start()
    {
        onTarget = false; 
        interaction_text = InteractionInfo.GetComponent<TextMeshProUGUI>(); 
        if (interaction_text == null)
        {
            Debug.LogError("not found");
        }
    }

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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>(); 

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                InteractionInfo.SetActive(true);                     // Включаем отображение информации о взаимодействии
                interaction_text.text = interactable.GetItemName();  // Устанавливаем текст информации о взаимодействии
                handIsVisible = true;
            }
            else
            {
                onTarget = false;
                InteractionInfo.SetActive(false);                    // Выключаем отображение информации о взаимодействии
                handIsVisible = false;
            }
        }
        else
        {
            onTarget = false;
            InteractionInfo.SetActive(false);
            handIsVisible = false;
        }
    }
}
