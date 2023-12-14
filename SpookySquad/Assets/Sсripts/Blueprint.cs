using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------- Скрипт для чертежей ----------------//

public class Blueprint : MonoBehaviour
{
    // поля для хранения данных о чертеже
    public string itemName;
    public int numOfRequirements;
    public string Req1;
    public int Req1amount;
    public string Req2;
    public int Req2amount;
    public string Req3;
    public int Req3amount;

    public void Initialize(string name, int numRequirements, string req1, int req1Amount, string req2, int req2Amount, string req3, int req3Amount)
    {
        itemName = name;
        numOfRequirements = numRequirements;
        Req1 = req1;
        Req1amount = req1Amount;
        Req2 = req2;
        Req2amount = req2Amount;
        Req3 = req3;
        Req3amount = req3Amount;
    }
}
