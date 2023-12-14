using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//---------------- Скрипт для инвентаря (на уровне объектов)----------------//

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isTrashable;       // Можно ли выбросить этот предмет
    public bool isConsumable;      // Можно ли потребить этот предмет
    public bool isEquippable;      // Можно ли экипировать этот предмет
    public bool isInsideQuickSlot; // Находится ли предмет в быстрых слотах
    public bool isSelected;        // Выбран ли этот предмет в инвентаре

    private GameObject itemInfoUI; // Интерфейс информации о предмете

    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality; // Информация о предмете full

    private GameObject itemPendingConsumption;

    public float healthEffect; // Эффект на здоровье при потреблении
    public float fearEffect;   // Эффект на рассудок при потреблении

    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUi; 
        itemInfoUI_itemName = itemInfoUI.transform.Find("itemName").GetComponent<Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("itemDescription").GetComponent<Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("itemFunctionality").GetComponent<Text>();
    }

    void Update()
    {
        if (isSelected)
        {
            gameObject.GetComponent<DragAndDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragAndDrop>().enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // При наведении мыши на предмет, отображаем информацию о нем
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // При уходе мыши с предмета, скрываем информацию о нем
        itemInfoUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, fearEffect);
            }

            if (isEquippable && !isInsideQuickSlot && !EquipSystem.Instance.CheckIfFull())
            {
                // Добавляем предмет в быстрые слоты, если он не там и слоты не заполнены
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                // Уничтожаем предмет после его потребления
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }

    private void consumingFunction(float healthEffect, float fearEffect)
    {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);
        fearEffectCalculation(fearEffect);
    }

    private static void healthEffectCalculation(float healthEffect)
    {
        // Вычисление эффекта на здоровье
        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.setHealth(maxHealth);
            }
            else
            {
                PlayerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }

    private static void fearEffectCalculation(float hydrationEffect)
    {
        // Вычисление эффекта на рассудок
        float fearBeforeConsumption = PlayerState.Instance.currentFear;
        float maxFear = PlayerState.Instance.maxFear;

        if (hydrationEffect != 0)
        {
            if ((fearBeforeConsumption + hydrationEffect) > maxFear)
            {
                PlayerState.Instance.setFear(maxFear);
            }
            else
            {
                PlayerState.Instance.setFear(fearBeforeConsumption + hydrationEffect);
            }
        }
    }
}
