using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    private CharacterMove characterMove; // 김채윤님 제작 능력치 스크립트 차후 반영 
    private PlayerHp playerHP; // 김채윤님 제작 PlayerHP.cs 추후 반영

    // 특성 효과 저장을 위한 딕셔너리
    private Dictionary<TraitType, System.Action<float>> traitEffects;

    public List<Trait> acquiredTraits = new List<Trait>();

    private void Start()
    {
        characterMove = FindObjectOfType<CharacterMove>(); // 플레이어 이동속도 스크립트 찾기
        playerHP = FindObjectOfType<PlayerHp>(); // 플레이어 HP 스크립트 찾기

        SetupTraitEffects(); // 특성 효과 딕셔너리 초기화
    }

    private void SetupTraitEffects()
    {
        traitEffects = new Dictionary<TraitType, System.Action<float>>
        {
            {
                TraitType.MoveSpeed, (value) =>
                {
                    if (characterMove != null)
                    {
                        characterMove.moveSpeed *= value;
                        Debug.Log($"이동속도 증가! 현재 이동속도 : {characterMove.moveSpeed}");
                    }
                }
            },

            {
                TraitType.MaxHealth, (value) =>
                {
                    if (playerHP != null)
                    {
                        playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * value);
                        Debug.Log($"최대체력 증가! 현재 최대체력 : {playerHP.MaxHealth}");
                    }
                }
            },

            {
                TraitType.AttackSpeed, (value) =>
                {
                    if (attackSpeed != null)
                    {

                    }
                }
            }
        }
    }

    public void ApplyTrait(Trait trait)
    {
        // 기존 특성과 겹치는지 확인해야 한다.
        Trait existingTrait = acquiredTraits.Find(t => t.type == trait.type);

        if (existingTrait != null)
        {
            Debug.Log($"{trait.traitName} 특성 강화!");

            // 이미 한번 가진 특성일 경우 추가 강화
            switch (trait.type)
            {
                case TraitType.MoveSpeed:
                    if (cha)
            }
        }

        switch (trait.type)
        {
            case TraitType.MoveSpeed:
                if (characterMove != null)
                {
                    characterMove.moveSpeed *= trait.value;
                    Debug.Log($"특성적용! 이동속도 {trait.value}배 증가!");
                }
                else
                {
                    Debug.LogWarning("[오류] CharacterMove를 찾지 못했습니다.");
                }
                break;
            case TraitType.MaxHealth:
                if (playerHP != null)
                {
                    playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * trait.value);
                    playerHP.CurrentHealth = playerHP.MaxHealth; // 특성 얻을때 체력을 풀로 채우기
                    Debug.Log("최대체력 증가!");
                }
                break;
            case TraitType.AttackPower:
                Debug.Log("공격력 증가!");
                break;
            case TraitType.AttackSpeed:
                Debug.Log("공격속도 증가!");
                break;
            case TraitType.ExtraProjectile:
                Debug.Log("추가 투사체 발사!");
                break;
            default:
                Debug.LogWarning("적용할 수 없는 특성입니다." + trait.traitName);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (acquiredTraits.Count > 0)
            {
                ApplyTrait(acquiredTraits[0]); // 리스트 첫번째 특성 적용
                Debug.Log("T키 첫 번째 특성 적용완료");
            }
        }
    }
}
