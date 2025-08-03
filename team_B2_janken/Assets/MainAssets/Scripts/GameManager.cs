using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int[] janken = new int[] { 0, 0, 0 }; //�v�f�i�O�j���O�[�A�v�f�i�P�j���`���L�A�v�f�i�Q�j���p�[
    private int StartFlag = 0;
    public GameObject[] Player1UI = new GameObject[] {};
    public GameObject[] Players = new GameObject[] {};

    public GameObject Resporn;

    //UI�̕ϐ�
    private int score = 0;
    public Text ScoreText_1;

    void Start()
    {
        
    }

    void Update()
    {
        if (StartFlag == 1)
        {
            if (janken[0] == 1)
            {
                Debug.Log("�ݒ肳�ꂽ��F�O�[");
                Players[0].gameObject.tag = "Rock";
                StartFlag = 0;
            }
            else if (janken[1] == 1)
            {
                Debug.Log("�ݒ肳�ꂽ��F�`���L");
                Players[0].gameObject.tag = "Scissors";
                StartFlag = 0;
            }
            else if (janken[2] == 1)
            {
                Debug.Log("�ݒ肳�ꂽ��F�p�[");
                Players[0].gameObject.tag = "Paper";
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

    public void OnScissors01()
    {
        janken[1] = 1;
        StartFlag = 1;
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
    }

    public void OnPaper01()
    {
        janken[2] = 1;
        StartFlag = 1;
        Player1UI[0].SetActive(false);
        Player1UI[1].SetActive(false);
        Player1UI[2].SetActive(false);
    }

    public void AddScore_1(int num)
    {
        score += num;
        ScoreText_1.text = score.ToString();
    }

    public void Resporn_test()
    {
        Players[0].transform.position = Resporn.transform.position;
        Players[0].transform.rotation = Resporn.transform.rotation; // ���������킹�����ꍇ
    }
}
