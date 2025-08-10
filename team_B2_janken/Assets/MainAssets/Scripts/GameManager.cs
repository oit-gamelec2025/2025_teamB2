using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] Players = new GameObject[] {};
    public GameObject[] Hands = new GameObject[] { };
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

    //自分の手

    //制限時間
    float totaltime = 6; //鬼ごっこが始まる前にも好きに移動するタイムがあるから
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
                totaltime = 121;
                retotaltime = 1;
                timeflag = 1;
                StartText.SetActive(true);
                int hand01 = Random.Range(0, 3); // 0以上10未満の整数（0〜9）
                int hand02 = Random.Range(0, 3);
                Players[0].gameObject.tag = hands[hand01];
                Players[1].gameObject.tag = hands[hand02];
                if(hand01 == 0)
                {
                    Rock01.SetActive(true);
                    Hands[0].SetActive(true);
                }
                else if(hand01 == 1)
                {
                    Scissors01.SetActive(true);
                    Hands[1].SetActive(true);
                }
                else if(hand01 == 2)
                {
                    Paper01.SetActive(true);
                    Hands[2].SetActive(true);
                }
                if (hand02 == 0)
                {
                    Rock02.SetActive(true);
                    Hands[3].SetActive(true);
                }
                else if (hand02 == 1)
                {
                    Scissors02.SetActive(true);
                    Hands[4].SetActive(true);
                }
                else if (hand02 == 2)
                {
                    Paper02.SetActive(true);
                    Hands[5].SetActive(true);
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
        if(totaltime < 119)
        {
            StartText.SetActive(false);
        }
    }

    //プレイヤー１のアイテム
    public void ChangeRock01()
    {
        Debug.Log("設定された手：グー");
        Players[0].gameObject.tag = "Rock";
        Rock01.SetActive(true);
        Scissors01.SetActive(false);
        Paper01.SetActive(false);
        Hands[0].SetActive(true);
        Hands[1].SetActive(false);
        Hands[2].SetActive(false);
    }

    public void ChangeScissors01()
    {
        Debug.Log("設定された手：チョキ");
        Players[0].gameObject.tag = "Scissors";
        Rock01.SetActive(false);
        Scissors01.SetActive(true);
        Paper01.SetActive(false);
        Hands[0].SetActive(false);
        Hands[1].SetActive(true);
        Hands[2].SetActive(false);
    }

    public void ChangePaper01()
    {
        Debug.Log("設定された手：パー");
        Players[0].gameObject.tag = "Paper";
        Rock01.SetActive(false);
        Scissors01.SetActive(false);
        Paper01.SetActive(true);
        Hands[0].SetActive(false);
        Hands[1].SetActive(false);
        Hands[2].SetActive(true);
    }

    //プレイヤー２のアイテム
    public void ChangeRock02()
    {
        Debug.Log("設定された手：グー");
        Players[1].gameObject.tag = "Rock";
        Rock02.SetActive(true);
        Scissors02.SetActive(false);
        Paper02.SetActive(false);
        Hands[3].SetActive(true);
        Hands[4].SetActive(false);
        Hands[5].SetActive(false);
    }

    public void ChangeScissors02()
    {
        Debug.Log("設定された手：チョキ");
        Players[1].gameObject.tag = "Scissors";
        Rock02.SetActive(false);
        Scissors02.SetActive(true);
        Paper02.SetActive(false);
        Hands[3].SetActive(false);
        Hands[4].SetActive(true);
        Hands[5].SetActive(false);
    }

    public void ChangePaper02()
    {
        Debug.Log("設定された手：パー");
        Players[1].gameObject.tag = "Paper";
        Rock02.SetActive(false);
        Scissors02.SetActive(false);
        Paper02.SetActive(true);
        Hands[3].SetActive(false);
        Hands[4].SetActive(false);
        Hands[5].SetActive(true);
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
