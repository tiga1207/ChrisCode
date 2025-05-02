using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Scripts.UI;

public class TitleButtonHandler : MonoBehaviour
{
    public Button[] buttons; // 여러 버튼을 배열로 관리
    public Stack<GameObject> uiStack;
    public GameObject titleUI;
    public GameObject exitGamePopup;
    public GameObject soundSettingUI;
    //public GameObject ExitGamePopupPrefab;

    void Start()
    {
        // 모든 버튼에 대해 이벤트를 동적으로 연결
        foreach (var button in buttons)
        {
            // 버튼 이름(문자열)을 Enum 값으로 변환하여 전달
            ButtonName btnName = (ButtonName)System.Enum.Parse(typeof(ButtonName), button.name);
            button.onClick.AddListener(() => OnButtonClicked(btnName)); // 버튼 이름을 기반으로 동작 결정
        }
    }

    private void OnButtonClicked(ButtonName btn)
    {
        switch (btn)
        {
            case ButtonName.StartButton:
                StartGame();
                break;
            case ButtonName.OptionButton:
                TMP_UIManager.Instance.OpenUI(soundSettingUI);
                break;
            case ButtonName.GameCloseButton:
                //TitleUI.SetActive(false);
                TMP_UIManager.Instance.OpenUI(exitGamePopup);
                Debug.Log("게임종료버튼누름");
                break;
            /*case ButtonName.ExitGameButton:
                //TODO - 게임 종료 구현되면 게임종료 함수 호출 쓰기
                Debug.Log("게임 실제 종료 함수 호출");
                break;
            case ButtonName.CancelButton:
                TMP_UIManager.Instance.CloseUI(ExitGamePopup);
                Debug.Log("안녕하세요");
                break;
            */
            default:
                Debug.Log("알 수 없는 버튼: " + btn);
                break;
        }
    }

    private void StartGame()
    {
        Debug.Log("게임 시작!");
    }

    private void PauseGame()
    {
        Debug.Log("게임 일시 정지!");
    }

    private void ExitGame()
    {
        Debug.Log("게임 종료!");
    }
}
