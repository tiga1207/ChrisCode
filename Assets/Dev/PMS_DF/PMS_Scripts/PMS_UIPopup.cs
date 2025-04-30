using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMS_UIPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvas; // 팝업 창의 캔버스
    // Start is called before the first frame update

    private void Start()
    {
        Close();
    }
    public void Open()
    {
        if(popupCanvas != null)
        {
            UIUtilities.SetUIActive(popupCanvas, true);
        }
    }

    public void Close()
    {
        if (popupCanvas != null)
        {
            UIUtilities.SetUIActive(popupCanvas, false);
        }
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
