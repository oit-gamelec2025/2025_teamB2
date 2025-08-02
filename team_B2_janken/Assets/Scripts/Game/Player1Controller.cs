using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public 

    void Start()
    {
        int[] selected = SettingManager.janken;

        if (selected[0] == 1)
        {
            Debug.Log("設定された手：グー");
            gameObject.tag = "Rock";
        }
        else if (selected[1] == 1)
        {
            Debug.Log("設定された手：チョキ");
            gameObject.tag = "Scissors";
        }
        else if (selected[2] == 1)
        {
            Debug.Log("設定された手：パー");
            gameObject.tag = "Paper";
        }
        else
        {
            Debug.Log("選択されていない");
        }
    }

    void Update()
    {
        
    }
}
