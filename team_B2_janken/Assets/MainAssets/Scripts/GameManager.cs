using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] Player1UI = new GameObject[] { };
    public GameObject[] Player2UI = new GameObject[] { };
    public GameObject[] Players = new GameObject[] {};
    public GameObject StartText;

    string[] hands = new string[] { "Rock", "Scissors", "Paper" };

    public GameObject Resporn01;
    public GameObject Resporn02;

    //武器
    public GameObject Rock01;
    public GameObject Scissors01;
    public GameObject Paper01;

    public GameObject Rock02;
    public GameObject Scissors02;
    public GameObject Paper02;


    //UIの変数
    int score01 = 0;
    public Text ScoreText_1;
    int score02 = 0;
    public Text ScoreText_2;
    public Text Win_1;
    public Text Lose_1;
    public Text Win_2;
    public Text Lose_2;
    public Text Draw;


    //制限時間
    float totaltime = 6;
    int retotaltime = 0;
    public Text TimerText;
    int ChoseFlag = 0;
    int timeflag = 0;

    void Start()
    {
        Rock01.SetActive(false);
        Scissors01.SetActive(false);
        Paper01.SetActive(false);
        Rock02.SetActive(false);
        Scissors02.SetActive(false);
        Paper02.SetActive(false);
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
                retotaltime = 1;
                timeflag = 1;
                StartText.SetActive(true);
                Player1UI[0].SetActive(false);
                Player1UI[1].SetActive(false);
                Player1UI[2].SetActive(false);
                Player2UI[0].SetActive(false);
                Player2UI[1].SetActive(false);
                Player2UI[2].SetActive(false);
                int num = Random.Range(0, 3); // 0以上10未満の整数（0〜9）
                Players[0].gameObject.tag = hands[num];
                Players[1].gameObject.tag = hands[num];
                if(num == 0)
                {
                    Rock01.SetActive(true);
                    Rock02.SetActive(true);
                }
                else if(num == 1)
                {
                    Scissors01.SetActive(true);
                    Scissors02.SetActive(true);
                }
                else if(num == 2)
                {
                    Paper01.SetActive(true);
                    Paper02.SetActive(true);
                }
            }
        }
        else if(timeflag == 1) //result
        {
            if(score01 > score02)
            {
                SceneManager.LoadScene("1PWinEnd");
            }
            else if(score01 < score02)
            {
                SceneManager.LoadScene("2PWinEnd");
            }
            else
            {
                SceneManager.LoadScene("DrawEnd");
            }
        }
        if(totaltime < 59)
        {
            StartText.SetActive(false);
        }
    }

    //Player1 UI
    public void OnRock01()
    {
        Debug.Log("設定された手：グー");
        Players[0].gameObject.tag = "Rock";
        totaltime = 61;
        timeflag = 1;
        StartText.SetActive(true);
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
        Rock01.SetActive(true);
        Scissors01.SetActive(false);
        Paper01.SetActive(false);
        ChoseFlag = 1;
    }

    public void OnScissors01()
    {
        Debug.Log("設定された手：チョキ");
        Players[0].gameObject.tag = "Scissors";
        totaltime = 61;
        timeflag = 1;
        StartText.SetActive(true);
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
        Rock01.SetActive(false);
        Scissors01.SetActive(true);
        Paper01.SetActive(false);
        ChoseFlag = 1;
    }

    public void OnPaper01()
    {
        Debug.Log("設定された手：パー");
        Players[0].gameObject.tag = "Paper";
        totaltime = 61;
        timeflag = 1;
        StartText.SetActive(true);
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
        Rock01.SetActive(false);
        Scissors01.SetActive(false);
        Paper01.SetActive(true);
        ChoseFlag = 1;
    }

    //Player2 UI
    public void OnRock02()
    {
        Debug.Log("設定された手：グー");
        Players[1].gameObject.tag = "Rock";
        Player2UI[0].SetActive(false);
        Player2UI[1].SetActive(false);
        Player2UI[2].SetActive(false);
        Rock02.SetActive(true);
        Scissors02.SetActive(false);
        Paper02.SetActive(false);
    }

    public void OnScissors02()
    {
        Debug.Log("設定された手：チョキ");
        Players[1].gameObject.tag = "Scissors";
        Player2UI[0].SetActive(false);
        Player2UI[1].SetActive(false);
        Player2UI[2].SetActive(false);
        Rock02.SetActive(false);
        Scissors02.SetActive(true);
        Paper02.SetActive(false);
    }

    public void OnPaper02()
    {
        Debug.Log("設定された手：パー");
        Players[1].gameObject.tag = "Paper";
        Player2UI[0].SetActive(false);
        Player2UI[1].SetActive(false);
        Player2UI[2].SetActive(false);
        Rock02.SetActive(false);
        Scissors02.SetActive(false);
        Paper02.SetActive(true);
    }

    //プレイヤー１のアイテム
    public void ChangeRock01()
    {
        Debug.Log("設定された手：グー");
        Players[0].gameObject.tag = "Rock";
        Rock01.SetActive(true);
        Scissors01.SetActive(false);
        Paper01.SetActive(false);
    }

    public void ChangeScissors01()
    {
        Debug.Log("設定された手：チョキ");
        Players[0].gameObject.tag = "Scissors";
        Rock01.SetActive(false);
        Scissors01.SetActive(true);
        Paper01.SetActive(false);
    }

    public void ChangePaper01()
    {
        Debug.Log("設定された手：パー");
        Players[0].gameObject.tag = "Paper";
        Rock01.SetActive(false);
        Scissors01.SetActive(false);
        Paper01.SetActive(true);
    }

    //プレイヤー２のアイテム
    public void ChangeRock02()
    {
        Debug.Log("設定された手：グー");
        Players[1].gameObject.tag = "Rock";
        Rock02.SetActive(true);
        Scissors02.SetActive(false);
        Paper02.SetActive(false);
    }

    public void ChangeScissors02()
    {
        Debug.Log("設定された手：チョキ");
        Players[1].gameObject.tag = "Scissors";
        Rock02.SetActive(false);
        Scissors02.SetActive(true);
        Paper02.SetActive(false);
    }

    public void ChangePaper02()
    {
        Debug.Log("設定された手：パー");
        Players[1].gameObject.tag = "Paper";
        Rock02.SetActive(false);
        Scissors02.SetActive(false);
        Paper02.SetActive(true);
    }

    public void AddScore_1(int num)
    {
        score01 += num;
        ScoreText_1.text = score01.ToString();
    }

    public void AddScore_2(int num)
    {
        score02 += num;
        ScoreText_2.text = score02.ToString();
    }

    public void Resporn_01()
    {
        Players[0].transform.position = Resporn01.transform.position;
        Players[0].transform.rotation = Resporn01.transform.rotation; // 向きも合わせたい場合
    }

    public void Resporn_02()
    {
        Players[1].transform.position = Resporn02.transform.position;
    }
}
