using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    //체력 관련
    [Header("Hp")]
    //체력
    [SerializeField] private int hp;
    public int HP{get{return hp;}}
    //최대 체력
    [SerializeField] public int maxHp;
    public int MaxHP{get{return maxHp;}}


    //공격 관련

    [Header("Attack Func")]
    //공격 데미지
    [SerializeField] private int attackDamage;
    //충돌(몸박) 데미지
    [SerializeField] private int collsionDamage;

    //공격 쿨타임 코루틴
    private Coroutine attackCoroutine;

    //공격 쿨타임
    [SerializeField] private float attackCooldown = 1f;
    //공격 가능 여부 변수
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isPlayerInAttackArea;


    //기타 스탯들 ~~~
    [Header("Status Etc")] 
    [SerializeField] private float moveSpeed;

    [Header("Tracking")]
    //플레이어 트래킹 여부
    [SerializeField] private bool isTrackingPlayer;
    private Transform playerPos;


    /*AI 및 추적 로직
    지속적 스폰
    다양한 타입의 적 구현
    */

    private void Start()
    {
        //씬 내의 플레이어 태그를 가진 오브젝트 1개를 찾기. (플레이어 2개시 해당 라인 변경 바람.)
        FindingPlayer();
    }

    private void FindingPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            playerPos = playerObj.transform;
        }
    }

    private void Update()
    {
        // // 플레이어 사망시 태그 해제 후 플레이어 부활시 플레이어 다시 찾기.
        // if(playerPos  == null)
        // {
        //     FindingPlayer();
        //     return;
        // }

        //공격범위 내에 있을 경우 플레이어를 바라보면서 공격, 아닐 경우 플레이어 위치 추척하며 이동
        if(!isPlayerInAttackArea)
        {
            MoveToPlayer();
        }
        else
        {
            LookPlayer();
        }

        if(isPlayerInAttackArea && canAttack)
        {
            AttackPlayer();
            canAttack= false;
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackCooldown());
            }
        }
    }
    #region 몬스터의 플레이어 추적 로직

    private void MoveToPlayer()
    {
        if(playerPos == null) return;
        Vector3 targetPos = new Vector3(playerPos.transform.position.x, transform.position.y, playerPos.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.LookAt(targetPos);
    }

    private void LookPlayer()
    {
        if(playerPos == null) return;
        Vector3 targetPos = new Vector3(playerPos.transform.position.x, transform.position.y, playerPos.transform.position.z);
        transform.LookAt(targetPos);
    }
    #endregion

    #region 몬스터 피격 & 공격 로직

    // 피해 받는 로직
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);
        
        //몬스터 체력 바 UI 이벤트 호출
        
        if(hp <=0)
        {
            MonsterDied();
        }

    }
    //공격 로직
    private void AttackPlayer()
    {
        Debug.Log($"{gameObject.name} : 플레이어에게 공격로직 실행");
        // if(player!=null)
        // {
        //     player.TakeDamage(attackDamage);
        // }

    }
    //공격 쿨타임 코루틴
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        attackCoroutine = null;
    }

    //공격범위 트리거
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInAttackArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInAttackArea = false;
        }
    }

    //몸박 로직
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //플레이어가 무적이 아닐 경우 -> 몸박 데미지 만큼 피해를 입게 함.
            // if(!player.invincible)
            // {
            //     player.TakeDamage(collsionDamage);
            // }
            //플레이어가 몬스터에게 몸박 데미지 아이템을 갖고 있는 경우
            // if(player.canBodyAttack)
            // {
            //     TakeDamage(player.atk);
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
