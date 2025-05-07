using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("PauseUI")]

    [SerializeField] private GameObject optionUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        //optionButton.onClick.AddListener();
        //restartButton.onClick.AddListener();
        //quitButton.onClick.AddListener();
    }

    private void OnContinueClicked()
    {
        //일시정지 해제 및 게임 진행
        //현재 Open된 UI 닫기
        TMP_UIManager.Instance.CloseCurrentUI();
    }

    private void OnOptionButtonClicked()
    {
        //옵션UI 뛰우게 해주고 
        TMP_UIManager.Instance.OpenUI(optionUI);
    }
}
