using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public int[] janken = new int[] { 0, 0, 0 }; //要素（０）がグー、要素（１）がチョキ、要素（２）がパー

    void Start()
    {
        
    }

    public void OnRock()
    {
        janken[0] = 1;
        if (janken[0] == 1)
        {
            Debug.Log("Rock!!");
        }
    }

    public void OnPaper()
    {
        janken[1] = 1;
        if (janken[1] == 1)
        {
            Debug.Log("Paper!!");
        }
    }

    public void OnScissors()
    {
        janken[2] = 1;
        if (janken[2] == 1)
        {
            Debug.Log("Scissors!!");
        }
    }
}
