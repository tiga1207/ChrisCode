using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{

    [Header("Hp")]
    //체력
    [SerializeField] private int hp;
    public int HP{get{return hp;} set{hp=value;}}
    //최대 체력
    [SerializeField] public int maxHp;
    public int MaxHP{get{return maxHp;} set{maxHp=value;}}

    [Header("Attack Func")]
    //공격 데미지
    private int attackDamage;
    //충돌(몸박) 데미지
    private int collsionDamage;

    //공격 쿨타임 코루틴
    private Coroutine attackCoroutine;

    //공격 쿨타임
    private float attackCooldown = 1f;

    //쿨타임 없이 지속적으로 데미지를 받아도 되면 아래 변수 삭제 요망.
    private bool isTakeDamage;

    [Header("Tracking")]
    //플레이어 트래킹 여부
    private bool isTrackingPlayer;
    // [Header("")]


    /*AI 및 추적 로직
    지속적 스폰
    다양한 타입의 적 구현
    */
    #region 몬스터의 플레이어 추적 로직
    private void TrackingPlayer()
    {
        
    }
    #endregion

    #region 몬스터 피격 & 공격 로직

    // 피해 받는 로직
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Mathf.Clamp(hp, 0, maxHp);
        
        //몬스터 체력 바 UI 이벤트 호출
        
        if(hp <=0)
        {
            MonsterDied();
        }

    }
    //공격 로직
    private void AttackPlayer()
    {

    }
    //공격 쿨타임 코루틴
    private IEnumerator AttackCooldown()
    {
        yield return null;
    }

    //몸박 로직
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //플레이어가 무적이 아닐 경우 -> 몸박 데미지 만큼 피해를 입게 함.
            // if(!player.invincible)
            // {
            //     player.TakeDamage(collsionDamage);
            // }
        }
    }


    #endregion

    #region 몬스터 생성 및 사망 로직
    //몬스터 생성 로직
    private void SpawnMonster()
    {

    }
    //몬스터 사망 로직
    private void MonsterDied()
    {
        //오브젝트 풀로 구현할 경우

        //아닐 경우
        Destroy(gameObject);
    }
    #endregion
}
