using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClosePopup : MonoBehaviour
{
    public Button exitGameButton;
    public Button cancelButton;
    private int count = 2;

    void Start()
    {
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    void OnExitGameButtonClicked()
    {
        //TODO - 게임 종료 구현되면 게임종료 함수 호출 쓰기
        Debug.Log(exitGameButton);
    }

    void OnCancelButtonClicked()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
        Debug.Log(cancelButton);
    }
}
