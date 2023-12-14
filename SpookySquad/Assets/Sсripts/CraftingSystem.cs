using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//---------------- ������ ��� ������ ----------------//

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; set; }

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    public bool isOpen;

    Button toolsBTN;

    Button craftSwordBTN;

    Text SwordReq1, SwordReq2, SwordReq3;

    Blueprint SwordBLP;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        isOpen = false;

        // ������� ������ "SwordButton" � ���������� � ��������� � ��� ����������
        toolsBTN = craftingScreenUI.transform.Find("SwordButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolCategory(); });

        // ������� ������ ���������� ��� ����
        SwordReq1 = toolsScreenUI.transform.Find("Sword").transform.Find("req1").GetComponent<Text>();
        SwordReq2 = toolsScreenUI.transform.Find("Sword").transform.Find("req2").GetComponent<Text>();
        SwordReq3 = toolsScreenUI.transform.Find("Sword").transform.Find("req3").GetComponent<Text>();

        // ������� � �������������� Blueprint ��� ����
        SwordBLP = gameObject.AddComponent<Blueprint>();
        SwordBLP.Initialize("Sword", 3, "Cross", 3, "Connector", 1, "Blade", 1);

        // ������� ������ "Craft" ��� ���� � ��������� � ��� ����������
        craftSwordBTN = toolsScreenUI.transform.Find("Sword").transform.Find("Button").GetComponent<Button>();
        craftSwordBTN.onClick.AddListener(delegate { CraftAnyItem(SwordBLP); });
    }

    void OpenToolCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        // ������ �������� �������� �� �������

        // ��������� ������� � ���������
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        // ������� ����������� ������� �� ���������
        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }
        else if (blueprintToCraft.numOfRequirements == 3)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req3, blueprintToCraft.Req3amount);
        }

        StartCoroutine(Calculate());
    }

    public IEnumerator Calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }

    void Update()
    {

        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            // ��������� ����� ������ � �������� ������
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            // ��������� ����� ������ � ��������������� ������
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        // ���������� ���������� � ����������� ��������

        int key_count = 0;
        int scope_count = 0;
        int Sword_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Cross":
                    key_count += 1;
                    break;
                case "Connector":
                    scope_count += 1;
                    break;
                case "Blade":
                    Sword_count += 1;
                    break;
            }
        }

        // ��������� ������ ����������
        SwordReq1.text = "3 Cross [" + key_count + "]";
        SwordReq2.text = "1 Connector [" + scope_count + "]";
        SwordReq3.text = "1 Blade [" + Sword_count + "]";

        // ���/���� ������ "Craft", � ����������� �� ������� ����������� ��������
        if (key_count >= 3 && scope_count >= 1 && Sword_count >= 1)
        {
            craftSwordBTN.gameObject.SetActive(true);
        }
        else
        {
            craftSwordBTN.gameObject.SetActive(false);
        }
    }
}
