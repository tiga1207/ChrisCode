using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerHp : MonoBehaviour
{
    public int m_CurrentHealth = 5;  // 처음 5로 시작
    public int m_MaxHealth = 5;
    public int m_LimitMaxHealth = 10;
    private bool m_IsUntouchable = false;
    private float m_UntouchableTime = 2f; 

    // 게임 시작 시 캐릭터의 체력을 현재 상한 최대 체력으로 설정
    void Start()
    {
        m_MaxHealth = 5;
        m_CurrentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // 플레이어 데미지 계산
    public void TakeDamage(int damage)
    {
        if (m_IsUntouchable == true)
        {
            return;
        }
        // 무적 시간
        m_CurrentHealth -= damage;
        StartCoroutine(UntouchableTime());

        if (m_CurrentHealth <= 0)
        {
            Debug.Log(" 게임 종료/ 돌아가기");
        }
    }

    // 플레이어 체력 회복 시 호출
    /*
    public void IncreasePlayerCurrentHealth(int amount)
    {
        // 체력 상자와의 충돌, 상점, 게임 이벤트에서 체력 회복 이벤트가 있을 경우 호출
        // 괄호
        m_CurrentHealth = Mathf.Min(m_CurrentHealth + 1, m_MaxHealth);
        m_CurrentHealth = m_MaxHealth;
    }
    */

    public void Heal(int amount)
    {
        m_CurrentHealth = Mathf.Min(m_CurrentHealth + amount, m_MaxHealth);
        Debug.Log($"체력 회복: +{amount}, 현재 체력: {m_CurrentHealth}/{m_MaxHealth}");
    }


    // 최대 체력 상승 및 상한체력으로 제한
    public void IncreasePlayerMaxHealth(int amount)
    {
        m_MaxHealth = Mathf.Min(m_MaxHealth + amount, m_LimitMaxHealth);
    }

    // 몬스터 충돌 시 피해를 줄 수 있는 아이템을 소지할 겨우 
    public void canBodyAttack()
    {
        // 공격 구현 후 만들기
        //playerAttack.
    }

    // 플레이어 피격 시 일정시간 무적 패턴
    public IEnumerator UntouchableTime()
    {
        m_IsUntouchable = true;
        yield return new WaitForSeconds(m_UntouchableTime);
        m_IsUntouchable = false;
    }

    // 플레이어 현 체력 확인
    public int GetCurrentHealth()
    {
        return m_CurrentHealth;
    }

    // 플레이어 최대 체력 확인
    public int GetMaxHealth()
    {
        return m_MaxHealth;
    }

}

