using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp , Level, Kill , Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    //임시 데이터 나중에 게임매니저에서 해당값들을 들고와야함
    float curExp = 3;  
    float maxExp = 10;
    int level = 999;
    int kill = 100;

    private float maxTime = 150;  //
    private float currentTime = 160;
    public float remainTime;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = 3;   //TODO - 현재 경험치 가져오기
                float maxExp = 10;  //TODO - 다음 레벨 경험치 가져오기
                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", level);    //TODO - 현재 레벨 불러오기
                break;

            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", kill);    //TODO - 현재 레벨 불러오기
                break;

            case InfoType.Time:
                remainTime = maxTime - currentTime;             //TODO -  값 설정 나중에
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                Debug.Log(remainTime);
                if (min >= 0 && sec >= 0)
                    myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                else
                {
                    myText.text = "00:00"; //더이상 시간이 흐르지 않게
                }
                break;

            case InfoType.Health:
                break;
        }
    }
}
