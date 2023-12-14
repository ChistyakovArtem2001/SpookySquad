using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------- Скрипт для анимации действия экипированным объектом ----------------//

[RequireComponent(typeof(Animator))] // гарантирует наличие компонента Animator на объекте

public class EquipableItem : MonoBehaviour
{
    public Animator animator; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) &&
            InventorySystem.Instance.isOpen == false &&
            SelectionManager.Instance.handIsVisible == false)
        {

            animator.SetTrigger("hit");
        }
    }
}
