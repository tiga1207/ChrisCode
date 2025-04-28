using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitUIManager
{
    // 특성 선택창 패널이 뜨게 만들고 그 순간 잠시동안 일시정지를 시킨뒤 UI에서 특성을 선택하면 반영됨과 동시에 게임 재 실행

    public GameObject traitUIPanel;

    public void OpenTraitSelection()
    {
        if (traitUIPanel != null)
        {
            traitUIPanel.SetActive(true);
            Time.timeScale = 0f; //일시 정지
        }
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
