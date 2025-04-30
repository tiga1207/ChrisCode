using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPatternMonster : MonsterBase
{
    private Vector3 m_direction;
    [SerializeField]private float m_timer =10f;

    protected override void Start()
    {
        base.Start();
        m_canTrackingPlayer = false;
    }
    public void InitDir(Vector3 targetDir)
    {
        Vector3 targetPos = new Vector3(targetDir.x, transform.position.y,targetDir.z);;
        transform.LookAt(targetPos);
        m_direction = transform.forward;
    }
    protected override void Update()
    {
        transform.position += m_direction * m_moveSpeed * Time.deltaTime;
        m_timer -= Time.deltaTime;
        if(m_timer <= 0)
        {
            PatternMonsterDie();
        }
    }
}
