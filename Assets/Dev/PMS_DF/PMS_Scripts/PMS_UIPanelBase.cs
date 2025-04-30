using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UIPanel을 구성하게
public class PMS_UIPanelBase : MonoBehaviour
{
    [SerializeField] private GameObject UIpanel; //특정 UIPanel을 껏다 키게 해주게 하기

    //UI요소를 보여주게 하는 함수
    public void Show()
    {
        UIUtilities.SetUIActive(UIpanel,true);
    }

    //UI요소를 안보여주게 하는 요소
    public void Hide()
    {
        UIUtilities.SetUIActive(UIpanel, false);
    }

    public void OnButtonClicked()
    {
        
    }

}

public static class UIUtilities
{
    // 게임 오브젝트의 활성화 상태를 설정하는 유틸리티 함수
    public static void SetUIActive(GameObject uiObject, bool isActive)
    {
        if (uiObject != null)
        {
            uiObject.SetActive(isActive);
        }
    }
}
