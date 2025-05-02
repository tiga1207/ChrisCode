using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPatternMonster : MonsterBase
{
    //TODO<김승태>직선패턴 몬스터는 어떠한 몬스터와도 충돌이 발생하지 않도록 해야함.
    private Vector3 m_direction;
    [SerializeField]private float m_timer =10f;

    protected override void Start()
    {
        base.Start();
        m_canTrackingPlayer = false;
    }
    public void InitDir(Vector3 targetDir)
    {
        Vector3 targetPos = new Vector3(targetDir.x, transform.position.y,targetDir.z);
        transform.LookAt(targetPos);
        m_direction = transform.forward;
    }

    protected override void InitStatus()
    {
        base.InitStatus();
        m_rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    protected override void Update()
    {
        MonsterAnimationController();

        m_timer -= Time.deltaTime;
        //TODO<김승태> 사망조건 추가 필요 ex) 벽에 충돌 시
        if(m_timer <= 0) 
        {
            // PatternMonsterDie();
            MonsterDied();
        }
        if(m_isMonsterDie == true) return;

        Vector3 velocity = m_direction * m_moveSpeed;
        m_rb.velocity = new Vector3(velocity.x, m_rb.velocity.y, velocity.z);

        // transform.position += m_direction * m_moveSpeed * Time.deltaTime;
    }
}
