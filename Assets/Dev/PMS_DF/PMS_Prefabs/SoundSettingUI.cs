using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundSettingUI : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private Button bgmButton;
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    //[SerializeField] private AudioSource bgmAudioSource; // 배경음악 AudioSource
    //[SerializeField] private AudioSource sfxAudioSource; // 효과음 AudioSource

    private void Awake()
    {
        // TODO - 시작씬이나 인게임에서 둘다 조정해야하는데 Value값을 다시 설정하게 해야함
        //먼저 사운드매니저에게 value값을 들고와야한다(현재 임시 디버깅용)
        bgmSlider.value = TMP_GameManager.bgmValue;
        sfxSlider.value = TMP_GameManager.sfxValue;
    }

    void Start()
    {
        //TODO -  사운드매니저에서 함수 들고오기

        bgmButton.onClick.AddListener(() => BgmbtnClick());
        sfxButton.onClick.AddListener(() => SfxbtnClick());
        closeButton.onClick.AddListener(() => ClosebtnClick());

        // TODO - 슬라이더 값 변경 시 호출될 함수 연결(사운드매니저에서 함수들고오기)
        bgmSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);
    }

    //Button 클릭 디버깅용
    private void BgmbtnClick()
    {
        Debug.Log("BgmbtnClick");
        //클릭을 통한 활성화 or 음소거 설정 되었는지 게임매니저에게 전달?
    }

    private void SfxbtnClick()
    {
        Debug.Log("SfxbtnClick");
        //클릭을 통한 활성화 or 음소거 설정 되었는지 게임매니저에게 전달?
    }

    private void ClosebtnClick()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
    }

    //BGM,SFX 슬라이더 조절 디버깅용

    public void OnBgmSliderValueChanged(float value)
    {
        Debug.Log("Bgm슬라이더 값이 변경되었습니다: " + value);
        TMP_GameManager.bgmValue = bgmSlider.value;
    }
    private void OnSfxSliderValueChanged(float value)
    {
        Debug.Log("Bgm슬라이더 값이 변경되었습니다: " + value);
        TMP_GameManager.sfxValue = sfxSlider.value;
    }
}
