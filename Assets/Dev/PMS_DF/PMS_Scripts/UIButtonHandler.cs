using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButtonHandler : MonoBehaviour
{
    public Button[] buttons; // 여러 버튼을 배열로 관리
    public GameObject TitleUI;
    public GameObject ExitGamePopup;

    void Start()
    {
        // 모든 버튼에 대해 이벤트를 동적으로 연결
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button.name)); // 버튼 이름을 기반으로 동작 결정
        }
    }

    void OnButtonClicked(string buttonName)
    {
        switch (buttonName)
        {
            case "StartButton":
                StartGame();
                break;
            case "OptionButton":
                Debug.Log("OptionButton Click");
                break;
            case "ExitButton":
                //TitleUI.SetActive(false);
                ExitGamePopup.SetActive(true);
                break;
            case "CancelButton":
                ExitGamePopup.SetActive(false);
                break;
            default:
                Debug.Log("알 수 없는 버튼: " + buttonName);
                break;
        }
    }

    void StartGame()
    {
        Debug.Log("게임 시작!");
    }

    void PauseGame()
    {
        Debug.Log("게임 일시 정지!");
    }

    void ExitGame()
    {
        Debug.Log("게임 종료!");
    }
}
