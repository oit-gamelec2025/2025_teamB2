using UnityEngine;


public class AnimationControll : MonoBehaviour
{

   [SerializeField] Animator MoveObject;
   void Start()
   {
       MoveObject.speed = 0;　// 最初の速度を0にすることで実行時動かない状態からスタートします
   }

   public void OnClickMove() // ボタンをクリックした時に実行するメソッド
   {
       MoveObject.speed = 1; 
       MoveObject.Play("next anime"); //Animationの名前を変更している場合はその名前に変更
   }
}