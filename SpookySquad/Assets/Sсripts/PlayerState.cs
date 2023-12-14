using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//---------------- ������ ���������� �� ������� ������ ----------------//


public class PlayerState : MonoBehaviour
{
    // --- Player Health --- //
    public float currentHealth; // ������� �������� ������
    public float maxHealth;     // ������������ �������� ������

    // --- Player Fear --- //
    public float currentFear; // ������� ������� �������� ������
    public float maxFear;     // ������������ ������� �������� ������

    public bool isFearActive; // ����, �����������, ������� �� ����� (�� �������� �������� ����-���)

    public static PlayerState Instance { get; set; } // ������ �� ��������� ������ PlayerState

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
        currentHealth = maxHealth;     // ������������� ������� �������� ������ ������ �������������
        currentFear = maxFear;         // ������������� ������� ������� �������� ������ ������ �������������

        StartCoroutine(decreseFear()); // ��������� �������� ��� ���������� ������ ������
    }

    IEnumerator decreseFear()
    {
        while (true)
        {
            currentFear -= 1;                    // ��������� ������� ������ �� 1
            yield return new WaitForSeconds(20); // ���� 20 ������ ����� ��������� �����������
        }
    }


    void Update()
    {
        // �������� ����
        // if(Input.GetKeyDown(KeyCode.N))
        // {
        //    currentHealth -= 10;
        // }
    }


    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }


    public void setFear(float newFear)
    {
        currentFear = newFear;
    }
}
