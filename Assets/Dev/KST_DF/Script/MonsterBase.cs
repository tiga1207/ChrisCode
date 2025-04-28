using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    //체력 관련
    [Header("Hp")]
    //체력
    [SerializeField] protected int hp = 10;
    public int HP{get{return hp;}}
    //최대 체력
    [SerializeField] protected int maxHp= 10;
    public int MaxHP{get{return maxHp;}}


    //공격 관련

    [Header("Attack Func")]
    //공격 데미지
    [SerializeField] protected int attackDamage = 2;
    //충돌(몸박) 데미지
    [SerializeField] protected int collsionDamage = 1;

    //공격 쿨타임 코루틴
    private Coroutine attackCoroutine;

    //공격 쿨타임
    [SerializeField] protected float attackCooldown = 1f;
    //공격 가능 여부 변수
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isPlayerInAttackArea = false;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] protected float attackRange =2f;


    //기타 스탯들 ~~~
    [Header("Status Etc")] 
    [SerializeField] protected float moveSpeed;

    [Header("Tracking")]
    //플레이어 트래킹 여부
    // [SerializeField] private bool isTrackingPlayer;
    [SerializeField]private Transform playerPos;


    /*AI 및 추적 로직
    지속적 스폰
    다양한 타입의 적 구현
    */

    void OnEnable()
    {
        FindingPlayer();
        //player.OnPlayerDied.AddListner(HandlePlayerDied)
    }
    void OnDisable()
    {
        //player.OnPlayerDied.RemoveListner(HandlePlayerDied)
        
    }

    protected virtual void Start()
    {
        //씬 내의 플레이어 태그를 가진 오브젝트 1개를 찾기. (플레이어 2개시 해당 라인 변경 바람.)
        FindingPlayer();
        if(sphereCollider !=null)
        {
            sphereCollider.radius = attackRange;
        }
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
        // 플레이어 객체 
        if(playerPos  == null)
        {
            Debug.Log("플레이어 어디감?");
            return;
        } 

            
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

    //플레이어 위치 & 방향
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

    //플레이어 사망 이벤트

    private void HandlePlayerDied()
    {
        Debug.Log("플레이어 사망 이벤트");
        //공격 사거리 및 위치 해제
        isPlayerInAttackArea = false;
        playerPos = null;
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
        if(playerPos != null)
        {
            Debug.Log($"{gameObject.name} : 플레이어에게 공격로직 실행");
            // if(player!=null)
            // {
            //     player.TakeDamage(attackDamage);
            // }

            //공격 이펙트 오브젝트 풀 패턴
        }



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
