using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerHp : MonoBehaviour
{
    public int CurrentHealth = 5;  // 처음 5로 시작
    public int MaxHealth = 5;
    public int LimitMaxHealth = 10;
    private bool isUntouchable = false;
    private float untouchableTime = 2f;

    private void Awake()
    {
        tag = "Player";
    }

    // 게임 시작 시 캐릭터의 체력을 현재 상한 최대 체력으로 설정
    void Start()
    {
        MaxHealth = 5;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 플레이어 데미지 계산
    public void TakeDamage(int damage)
    {
        if (IsUntouchable == true)
        {
            return;
        }
        // 무적 시간
        CurrentHealth -= damage;
        StartCoroutine(UntouchableTime());

        if (CurrentHealth <= 0)
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
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        Debug.Log($"체력 회복: +{amount}, 현재 체력: {CurrentHealth}/{MaxHealth}");
    }


    // 최대 체력 상승 및 상한체력으로 제한
    public void IncreasePlayerMaxHealth(int amount)
    {
        MaxHealth = Mathf.Min(MaxHealth + amount, LimitMaxHealth);
    }

    // 몬스터 충돌 시 피해를 줄 수 있는 아이템을 소지할 겨우 
    public void CanBodyAttack()
    {
        // 공격 구현 후 만들기
        //playerAttack.
    }

    // 플레이어 피격 시 일정시간 무적 패턴
    public IEnumerator UntouchableTime()
    {
        IsUntouchable = true;
        yield return new WaitForSeconds(untouchableTime);
        IsUntouchable = false;
    }

    // 플레이어 현 체력 확인
    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    // 플레이어 최대 체력 확인
    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public bool IsUntouchable
    {
        get => isUntouchable;
        set
        {
            if (value && !isUntouchable)
            {
                isUntouchable = true;
                StartCoroutine(UntouchableTime());
            }
        }
    }
}
