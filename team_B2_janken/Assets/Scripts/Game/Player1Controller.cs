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
            Debug.Log("�ݒ肳�ꂽ��F�O�[");
            gameObject.tag = "Rock";
        }
        else if (selected[1] == 1)
        {
            Debug.Log("�ݒ肳�ꂽ��F�`���L");
            gameObject.tag = "Scissors";
        }
        else if (selected[2] == 1)
        {
            Debug.Log("�ݒ肳�ꂽ��F�p�[");
            gameObject.tag = "Paper";
        }
        else
        {
            Debug.Log("�I������Ă��Ȃ�");
        }
    }

    void Update()
    {
        
    }
}
