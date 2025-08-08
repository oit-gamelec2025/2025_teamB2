using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorChanger : MonoBehaviour
{
    public GameObject[] operatorObjects; // 0から9までの数字3Dオブジェクトを格納する配列

    void Start()
    {
        // 全ての数字オブジェクトを最初は非表示にする
        foreach (GameObject obj in operatorObjects)
        {
            obj.SetActive(false);
        }

        // 1秒ごとに数字を切り替えるコルーチンを開始
        StartCoroutine(ChangeOperatorRandomly());
    }

    IEnumerator ChangeOperatorRandomly()
    {
        int currentIndex = -1;
        while (true)
        {
            // 現在表示されているオブジェクトを非表示にする
            if (currentIndex != -1)
            {
                operatorObjects[currentIndex].SetActive(false);
            }

            // 正か負かランダムな符号を生成
            int randomIndex = Random.Range(0, 2);

            // ランダムな符号に対応するオブジェクトを表示する
            operatorObjects[randomIndex].SetActive(true);
            currentIndex = randomIndex;

            // 1秒待つ
            yield return new WaitForSeconds(1.0f);
        }
    }
}
