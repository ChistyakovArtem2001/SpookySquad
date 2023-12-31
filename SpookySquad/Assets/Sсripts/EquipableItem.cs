using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------- ������ ��� �������� �������� ������������� �������� ----------------//

[RequireComponent(typeof(Animator))] // ����������� ������� ���������� Animator �� �������

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
