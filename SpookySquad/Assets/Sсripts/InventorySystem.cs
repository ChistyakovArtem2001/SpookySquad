using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//---------------- Скрипт для инвентаря (на уровне системы) ----------------//

public class InventorySystem : MonoBehaviour
{
    public GameObject ItemInfoUi; 

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI; 

    public List<GameObject> slotList = new List<GameObject>(); 

    public List<string> itemList = new List<string>(); 

    private GameObject itemToAdd; 

    private GameObject whatSlotToEquip; 

    public bool isOpen; 

    public bool CheckForItem(string itemName)
    {
        return itemList.Contains(itemName);
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

    void Start()
    {
        isOpen = false;

        PopulateSlotList();

        Cursor.visible = false;
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            slotList.Add(child.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();

        // Создаем предмет и добавляем его в слот
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName); // Добавляем предмет в список

        //ReCalculateList();
        //CraftingSystem.Instance.RefreshNeededItems();
    }

    private GameObject FindNextEmptySlot()
    {
        // Находим первый свободный слот для предмета
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        // Проверяем, заполнен ли инвентарь
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter -= 1;
                }
            }
        }

        //ReCalculateList();
        //CraftingSystem.Instance.RefreshNeededItems();
    }

    public void ReCalculateList()
    {
        // Пересчитываем список предметов в инвентаре
        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");
                itemList.Add(result);
            }
        }
    }
}
