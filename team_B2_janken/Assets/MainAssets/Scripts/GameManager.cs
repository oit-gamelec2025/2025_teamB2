using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Player1UI = new GameObject[] {};
    public GameObject[] Players = new GameObject[] {};
    public GameObject StartText;

    string[] hands = new string[] { "Rock", "Scissors", "Paper" };

    public GameObject Resporn;

    //武器
    public GameObject Rock;
    public GameObject Scissors;
    public GameObject Paper;


    //UIの変数
    int score = 0;
    public Text ScoreText_1;

    //制限時間
    float totaltime = 6;
    int retotaltime = 0;
    public Text TimerText;
    int ChoseFlag = 0;

    void Start()
    {
        Rock.SetActive(false);
        Scissors.SetActive(false);
        Paper.SetActive(false);
    }

    void Update()
    {
        //制限時間処理
        totaltime -= Time.deltaTime;
        retotaltime = (int)totaltime;
        if (retotaltime > -1)
        {
            TimerText.text = retotaltime.ToString();
            if(retotaltime == 0 && ChoseFlag == 0)
            {
                ChoseFlag = 1;
                totaltime = 61;
                StartText.SetActive(true);
                Player1UI[0].SetActive(false);
                Player1UI[1].SetActive(false);
                Player1UI[2].SetActive(false);
                int num = Random.Range(0, 3); // 0以上10未満の整数（0〜9）
                Players[0].gameObject.tag = hands[num];
                if(num == 0)
                {
                    Rock.SetActive(true);
                }
                else if(num == 1)
                {
                    Scissors.SetActive(true);
                }
                else if(num == 2)
                {
                    Paper.SetActive(true);
                }
            }
        }
        if(totaltime < 59)
        {
            StartText.SetActive(false);
        }
    }

    public void OnRock01()
    {
        Debug.Log("設定された手：グー");
        Players[0].gameObject.tag = "Rock";
        totaltime = 61;
        StartText.SetActive(true);
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
        Rock.SetActive(true);
        ChoseFlag = 1;
    }

    public void OnScissors01()
    {
        Debug.Log("設定された手：チョキ");
        Players[0].gameObject.tag = "Scissors";
        totaltime = 61;
        StartText.SetActive(true);
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
        Scissors.SetActive(true);
        ChoseFlag = 1;
    }

    public void OnPaper01()
    {
        Debug.Log("設定された手：パー");
        Players[0].gameObject.tag = "Paper";
        totaltime = 61;
        StartText.SetActive(true);
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
        Paper.SetActive(true);
        ChoseFlag = 1;
    }

    public void AddScore_1(int num)
    {
        score += num;
        ScoreText_1.text = score.ToString();
    }

    public void Resporn_test()
    {
        Players[0].transform.position = Resporn.transform.position;
        Players[0].transform.rotation = Resporn.transform.rotation; // 向きも合わせたい場合
    }
}
