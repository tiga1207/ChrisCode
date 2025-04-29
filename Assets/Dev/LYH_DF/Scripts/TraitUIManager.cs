using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitUIManager : MonoBehaviour
{
    // 특성 선택창 패널이 뜨게 만들고 그 순간 잠시동안 일시정지를 시킨뒤 UI에서 특성을 선택하면 반영됨과 동시에 게임 재 실행

    public GameObject traitUIPanel;
    public TraitButton[] traitButtons;

    public void OpenTraitSelection(List<Trait> traits)
    {
        if (traitUIPanel != null)
        {
            traitUIPanel.SetActive(true);
            Time.timeScale = 0f; //일시 정지

            for (int i = 0; i < traitButtons.Length; i++)
            {
                if (i < traits.Count)
                {
                    traitButtons[i].gameObject.SetActive(true); // 버튼 활성화
                    traitButtons[i].Setup(traits[i], OnTraitSelected);
                }
                else
                {
                    traitButtons[i].gameObject.SetActive(false); // 필요 없는 버튼 비활성화
                }
            }
        }
    }

    public void OnTraitSelected(Trait selectedTrait)
    {
        // TraitManager 찾아서 적용하기
        TraitManager traitManager = FindObjectOfType<TraitManager>();

        if (traitManager != null)
        {
            traitManager.ApplyTrait(selectedTrait);
        }

        CloseTraitSelection();
    }

    public void CloseTraitSelection()
    {
        if (traitUIPanel != null)
        {
            traitUIPanel.SetActive(false);
            Time.timeScale = 1f; //게임 재개
        }
    }

}
