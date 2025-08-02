using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] janken = new int[] { 0, 0, 0 }; //要素（０）がグー、要素（１）がチョキ、要素（２）がパー
    private int StartFlag = 0;
    public GameObject[] Player1UI = new GameObject[] {};
    public GameObject[] Players = new GameObject[] {};
    void Start()
    {
        
    }

    void Update()
    {
        if (StartFlag == 1)
        {
            if (janken[0] == 1)
            {
                Debug.Log("設定された手：グー");
                Players[0].gameObject.tag = "Rock";
                StartFlag = 0;
            }
            else if (janken[1] == 1)
            {
                Debug.Log("設定された手：チョキ");
                Players[1].gameObject.tag = "Scissors";
                StartFlag = 0;
            }
            else if (janken[2] == 1)
            {
                Debug.Log("設定された手：パー");
                Players[2].gameObject.tag = "Paper";
                StartFlag = 0;
            }
        }
    }

    public void OnRock01()
    {
        janken[0] = 1;
        StartFlag = 1;
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
    }

    public void OnPaper01()
    {
        janken[1] = 1;
        StartFlag = 1;
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
    }

    public void OnScissors01()
    {
        janken[2] = 1;
        StartFlag = 1;
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
    }
}
