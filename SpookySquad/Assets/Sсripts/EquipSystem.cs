using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//---------------- Скрипт для быстрых слотов ----------------//

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    //public List<string> itemList = new List<string>();

    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject toolHolder;

    public GameObject selectionItemModel;

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

    private void Start()
    {
        // Заполняем список слотов для быстрого доступа
        PopulateSlotList();
    }

    private void Update()
    {
        // Обработка нажатий клавиш для выбора слотов
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
    }

    void SelectQuickSlot(int numder)
    {
        if (checkIfSlotFull(numder))
        {
            if (selectedNumber != numder)
            {
                selectedNumber = numder;

                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = GetSelectedItem(numder);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquippedModel(selectedItem);

                // cменить цвет номера выбранного слота на зеленый
                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("text").GetComponent<Text>().color = Color.white;
                }
                Text toBeChanged = numbersHolder.transform.Find("number" + numder).transform.Find("text").GetComponent<Text>();
                toBeChanged.color = Color.green;
            }
            else
            {
                selectedNumber = -1;
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }
                if (selectionItemModel != null)
                {
                    DestroyImmediate(selectionItemModel.gameObject);
                    selectionItemModel = null;
                }
                // cменить цвет номера обратно на белый
                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("text").GetComponent<Text>().color = Color.white;
                }
            }
        }
    }

    private void SetEquippedModel(GameObject selectedItem)
    {
        if (selectionItemModel != null)
        {
            DestroyImmediate(selectionItemModel.gameObject);
            selectionItemModel = null;
        }
        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selectionItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"),
            new Vector3(0.6f, 0, 0.4f), Quaternion.Euler(0, -12.5f, -20f));
        selectionItemModel.transform.SetParent(toolHolder.transform, false);
    }

    GameObject GetSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    bool checkIfSlotFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PopulateSlotList()
    {
        // находим и добавляем в список слоты из интерфейса
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // находим следующий свободный слот и добавляем в него объект
        GameObject availableSlot = FindNextEmptySlot();
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // получаем чистое имя объекта
        string cleanName = itemToEquip.name.Replace("(Clone)", "");

        // добавляем объект в список
        //itemList.Add(cleanName);

        // пересчитываем инвентарь
        InventorySystem.Instance.ReCalculateList();
    }

    private GameObject FindNextEmptySlot()
    {
        // находим первый свободный слот для добавления объекта
        foreach (GameObject slot in quickSlotsList)
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
        // проверяем, заполнены ли все слоты
        int counter = 0;
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }
        if (counter == 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
