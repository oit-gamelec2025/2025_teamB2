using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SettingManager : MonoBehaviour
{
    public static int[] janken = new int[] { 0, 0, 0 }; //�v�f�i�O�j���O�[�A�v�f�i�P�j���`���L�A�v�f�i�Q�j���p�[
    private int StartFlag = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(StartFlag == 1)
        {
            SceneManager.LoadScene("game");
        }
    }

    public void OnRock()
    {
        janken[0] = 1;
        StartFlag = 1;
    }

    public void OnPaper()
    {
        janken[1] = 1;
        StartFlag = 1;
    }

    public void OnScissors()
    {
        janken[2] = 1;
        StartFlag = 1;
    }
}
