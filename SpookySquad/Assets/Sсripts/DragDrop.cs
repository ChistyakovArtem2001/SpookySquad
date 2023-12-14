using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//----------------  Скрипт для перетаскивания предметов в инвентаре ----------------//

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform inventory;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    private void Awake()
    {
        // получаем ссылки на компоненты RectTransform и CanvasGroup объекта
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("OnBeginDrag");

        // устанавливаем прозрачность объекта, чтобы он выглядел полупрозрачным во время перетаскивания
        canvasGroup.alpha = .6f;

        // блокируем обнаружение лучами объекта, который перетаскивается
        canvasGroup.blocksRaycasts = false;

        // сохраняем начальную позицию и родительский объект перед перемещением
        startPosition = transform.position;
        startParent = transform.parent;

        // переносим объект в корневой объект сцены для корректной видимости
        transform.SetParent(transform.root);

        // устанавливаем объект как "перетаскиваемый"
        itemBeingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // перемещаем объект вместе с мышью с учетом изменений позиции указателя
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // очищаем ссылку на объект, который перетаскивается
        itemBeingDragged = null;

        // проверяем, вернулся ли объект в исходное положение или родительский объект
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            // возвращаем объект на начальную позицию и в исходного родителя
            transform.position = startPosition;
            transform.SetParent(startParent);
        }

        //Debug.Log("OnEndDrag");

        // восстанавливаем прозрачность и обнаружение лучами объекта
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
