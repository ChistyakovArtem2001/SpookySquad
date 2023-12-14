using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//---------------- ������ ��� ��������� (�� ������ ��������)----------------//

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isTrashable;       // ����� �� ��������� ���� �������
    public bool isConsumable;      // ����� �� ��������� ���� �������
    public bool isEquippable;      // ����� �� ����������� ���� �������
    public bool isInsideQuickSlot; // ��������� �� ������� � ������� ������
    public bool isSelected;        // ������ �� ���� ������� � ���������

    private GameObject itemInfoUI; // ��������� ���������� � ��������

    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality; // ���������� � �������� full

    private GameObject itemPendingConsumption;

    public float healthEffect; // ������ �� �������� ��� �����������
    public float fearEffect;   // ������ �� �������� ��� �����������

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
        // ��� ��������� ���� �� �������, ���������� ���������� � ���
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ��� ����� ���� � ��������, �������� ���������� � ���
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
                // ��������� ������� � ������� �����, ���� �� �� ��� � ����� �� ���������
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
                // ���������� ������� ����� ��� �����������
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
        // ���������� ������� �� ��������
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
        // ���������� ������� �� ��������
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
