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
        }
        else if (selected[1] == 1)
        {
            Debug.Log("�ݒ肳�ꂽ��F�`���L");
        }
        else if (selected[2] == 1)
        {
            Debug.Log("�ݒ肳�ꂽ��F�p�[");
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
