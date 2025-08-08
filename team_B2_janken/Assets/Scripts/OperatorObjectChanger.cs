using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorObjectChanger : MonoBehaviour
{
    public GameObject[] operatorObjects; // 4つの演算子3Dオブジェクトを格納する配列
    private int currentIndex = -1;

    void Start()
    {
        // 全ての数字オブジェクトを最初は非表示にする
        foreach (GameObject obj in operatorObjects)
        {
            obj.SetActive(false);
        }

        // ゲーム開始時にランダムな演算子を一度だけ表示
        ChangeOperatorRandomly();
    }

    public void ChangeOperatorRandomly()
    {
        // 現在表示されているオブジェクトを非表示にする
        if (currentIndex != -1)
        {
            operatorObjects[currentIndex].SetActive(false);
        }

        // 0から3までのランダムな数字を生成
        int randomIndex = Random.Range(0, operatorObjects.Length);

        // ランダムな数字に対応するオブジェクトを表示する
        operatorObjects[randomIndex].SetActive(true);
        currentIndex = randomIndex;
    }
}
