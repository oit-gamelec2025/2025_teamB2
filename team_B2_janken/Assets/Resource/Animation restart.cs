using UnityEngine;


public class Animationrestart : MonoBehaviour
{

   [SerializeField] Animator MoveObject;
   void Start()
   {
       MoveObject.speed = 0;�@// �ŏ��̑��x��0�ɂ��邱�ƂŎ��s�������Ȃ���Ԃ���X�^�[�g���܂�
   }

   public void OnClickMove() // �{�^�����N���b�N�������Ɏ��s���郁�\�b�h
   {
       MoveObject.speed = 1; 
       MoveObject.Play("restartanime"); //Animation�̖��O��ύX���Ă���ꍇ�͂��̖��O�ɕύX
   }
}