using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//---------------- Скрипт для взаимодействия инвнтарь - быстрый слот ----------------//

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Если слот пуст, перемещаем предмет в этот слот
        if (!Item)
        {
            DragAndDrop.itemBeingDragged.transform.SetParent(transform);
            DragAndDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);

            if (transform.CompareTag("QuickSlot") == false)
            {
                // Если предмет перемещается из слота быстрого доступа, обновляем список предметов в инвентаре
                DragAndDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = false;
                InventorySystem.Instance.ReCalculateList();
            }

            if (transform.CompareTag("QuickSlot"))
            {
                // Если предмет перемещается в слот быстрого доступа, обновляем список предметов в инвентаре
                DragAndDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = true;
                InventorySystem.Instance.ReCalculateList();
            }
        }
    }
}
