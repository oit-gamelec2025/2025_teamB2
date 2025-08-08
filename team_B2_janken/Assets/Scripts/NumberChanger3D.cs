using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberChanger3D : MonoBehaviour
{
    public GameObject[] numberObjects; // 0から9までの数字3Dオブジェクトを格納する配列

    void Start()
    {
        // 全ての数字オブジェクトを最初は非表示にする
        foreach (GameObject obj in numberObjects)
        {
            obj.SetActive(false);
        }

        // 1秒ごとに数字を切り替えるコルーチンを開始
        StartCoroutine(ChangeNumberRandomly());
    }

    IEnumerator ChangeNumberRandomly()
    {
        int currentIndex = -1;
        while (true)
        {
            // 現在表示されているオブジェクトを非表示にする
            if (currentIndex != -1)
            {
                numberObjects[currentIndex].SetActive(false);
            }

            // 0から9までのランダムな数字を生成
            int randomIndex = Random.Range(0, 10);

            // ランダムな数字に対応するオブジェクトを表示する
            numberObjects[randomIndex].SetActive(true);
            currentIndex = randomIndex;

            // 1秒待つ
            yield return new WaitForSeconds(1.0f);
        }
    }
}
