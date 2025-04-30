using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일반 몬스터 타입
public enum MonsterType{Goblin, Skeleton, Etc}

public class MonsterBase : MonoBehaviour
{
    //체력 관련
    [Header("Hp")]
    //체력
    [SerializeField] protected int m_hp = 10;
    public int HP => m_hp;
    //최대 체력
    [SerializeField] protected int m_maxHp= 10;
    public int MaxHP => m_maxHp;


    //공격 관련

    [Header("Attack Func")]
    //공격 데미지
    [SerializeField] protected int m_attackDamage = 2;
    //충돌(몸박) 데미지
    [SerializeField] protected int m_collsionDamage = 1;

    //공격 쿨타임
    [SerializeField] protected float m_attackCooldown = 1f;
    //공격 쿨타임 코루틴
    private Coroutine m_attackCoroutine;

    //공격 가능 여부 변수
    [SerializeField] private bool m_canAttack = true;
    [SerializeField] private bool m_isPlayerInAttackArea = false;
    [SerializeField] private SphereCollider m_sphereCollider;
    [SerializeField] protected float m_attackRange =2f;


    //기타 스탯들 ~~~
    [Header("Status Etc")] 
    [SerializeField] protected float m_moveSpeed;
    public MonsterType monsterType;

    [Header("Tracking")]
    [SerializeField]private Transform m_playerPos;
    //플레이어 추적 기능 가진 몬스터 변수 
    // (기본값: true, 원형포진이거나, 일정 방향으로 날아가는 몬스터는 false)
    [SerializeField]protected bool m_canTrackingPlayer= true;


    /*AI 및 추적 로직
    지속적 스폰
    다양한 타입의 적 구현
    */

    void OnEnable()
    {
        FindingPlayer();
        InitStatus();

        //TODO<김승태> - 플레이어 죽음 이벤트 등록 필요
        //player.OnPlayerDied.AddListner(HandlePlayerDied)
    }

    protected virtual void OnDisable()
    {
        if(m_attackCoroutine !=null)
        {
            StopCoroutine(m_attackCoroutine);
            m_attackCoroutine =null;
        }
        m_isPlayerInAttackArea = false;
        m_canAttack=true;
        
        //TODO<김승태> - 플레이어 죽음 이벤트 해제 필요 - 20250430
        //player.OnPlayerDied.RemoveListener(HandlePlayerDied);
        
    }

    protected virtual void Start()
    {
        //씬 내의 플레이어 태그를 가진 오브젝트 1개를 찾기. (플레이어 2개시 해당 라인 변경 바람.)
        FindingPlayer();
        if(m_sphereCollider !=null)
        {
            m_sphereCollider.radius = m_attackRange;
        }
    }

    protected virtual void Update()
    {
        // 플레이어 객체 
        if(m_playerPos  == null) return;
            
        //공격범위 내에 있을 경우 플레이어를 바라보면서 공격, 아닐 경우 플레이어 위치 추척하며 이동
        if(m_canTrackingPlayer == true) // 플레이어를 추적하는 몬스터의 경우만.
        {
            if(m_isPlayerInAttackArea == false)
            {
                MoveToPlayer();
            }
            else
            {
                LookPlayer();
            }
        }

        if(m_isPlayerInAttackArea == true && m_canAttack ==true)
        {
            AttackPlayer();
            m_canAttack= false;
            if (m_attackCoroutine == null)
            {
                m_attackCoroutine = StartCoroutine(IE_AttackCooldown());
            }
        }
    }

    protected virtual void InitStatus()
    {
        m_hp = m_maxHp;
        m_isPlayerInAttackArea = false;
        m_canAttack= true;
    }
    protected virtual void FindingPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            m_playerPos = playerObj.transform;
        }
    }
    #region 몬스터의 플레이어 추적 로직

    //플레이어 위치 & 방향
    protected virtual void MoveToPlayer()
    {
        if(m_playerPos == null) return;
        Vector3 targetPos = new Vector3(m_playerPos.transform.position.x, transform.position.y, m_playerPos.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, m_moveSpeed * Time.deltaTime);
        transform.LookAt(targetPos);
    }

    protected virtual void LookPlayer()
    {
        if(m_playerPos == null) return;
        Vector3 targetPos = new Vector3(m_playerPos.transform.position.x, transform.position.y, m_playerPos.transform.position.z);
        transform.LookAt(targetPos);
    }

    //플레이어 사망 이벤트
    protected virtual void HandlePlayerDied()
    {
        Debug.Log("플레이어 사망 이벤트");
        //공격 사거리 및 위치 해제
        m_isPlayerInAttackArea = false;
        m_playerPos = null;
    }


    #endregion

    #region 몬스터 피격 & 공격 로직

    // 피해 받는 로직
    public void TakeDamage(int damage)
    {
        m_hp -= damage;
        m_hp = Mathf.Clamp(m_hp, 0, m_maxHp);
        
        //TODO<김승태> 필요시: 몬스터 체력 바 UI 이벤트 호출
        
        if(m_hp <=0)
        {
            MonsterDied();
        }

    }
    //공격 로직
    protected virtual void AttackPlayer()
    {
        if(m_playerPos != null)
        {
            Debug.Log($"{gameObject.name} : 플레이어에게 공격로직 실행");
            //TODO<김승태> 플레이어 있을 경우 데미지 로직
            // if(player!=null)
            // {
            //     player.TakeDamage(attackDamage);
            // }

            //공격 이펙트 오브젝트 풀 패턴
        }



    }
    //공격 쿨타임 코루틴
    private IEnumerator IE_AttackCooldown()
    {
        yield return new WaitForSeconds(m_attackCooldown);
        m_canAttack = true;
        m_attackCoroutine = null;
    }

    //공격범위 트리거
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_isPlayerInAttackArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_isPlayerInAttackArea = false;
        }
    }

    //몸박 로직
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //플레이어가 존재할 경우-> 몸박 데미지 만큼 피해를 입게 함.
            // if(player!=null)
            // {
            //     player.TakeDamage(collsionDamage);
            // }
            //플레이어가 몬스터에게 몸박 데미지 아이템을 갖고 있는 경우
            // if(player.canBodyAttack)
            // {
                // TakeDamage(player.atk);
                TakeDamage(10);
                Debug.Log("데미지 입음");
            // }
        }
    }


    #endregion

    #region 몬스터 생성 및 사망 로직
    //몬스터 사망 로직
    private void MonsterDied()
    {
        MonsterPoolManager.s_instance.ReturnPool(this);
    }
    #endregion
}
