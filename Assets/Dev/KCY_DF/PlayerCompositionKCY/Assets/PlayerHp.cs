using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public int CurrentHealth = 5;  // 처음 5로 시작
    public int MaxHealth = 5;
    public int LimitMaxHealth = 10;


    // 몬스터에게 데미지를 받는 경우  Update() 부분에서 조건 (부딪히기 또는 몬스터가 쏘는 공격에 부딪히기)
    public void TakeDamage()
    {
        CurrentHealth -= 1;
        if (CurrentHealth <= 0)
        {
            Debug.Log(" 게임 종료/ 돌아가기");
        }
    }

    // 풀레이어에 들어있는 코드,  collision(몬스터)에 부딪힐 경우 에이치피를 받는다.
    // 플레이어가 몇초 뒤에 다시 데미지를 받을 것인지, 데미지로인한 넉백 생각해보기
    public void OnCollisionEnter(Collision collision)
    {


        // 몬스터 공격 또는 몬스터 충돌 시에만 반응
        /*public void OnCollisionEnter(Collision collision)
         * {
         *    string tag = collison.gameObject.tag
         *    if(tag == Monster || MonsterAttack )
         *      {
         *         CurrentHealth -= 1;
                     if (CurrentHealth <= 0)
                         {
                             Debug.Log(" 게임 종료/ 돌아가기");
                         }
         *      }
         *  }
        */


        // 게임 시작 시 캐릭터의 체력을 현재 상한 최대 체력으로 설정
        void Start()
        {
            MaxHealth = 5;
            CurrentHealth = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            /*
             * if()
             *
             * 
            */

        }
    }
}
