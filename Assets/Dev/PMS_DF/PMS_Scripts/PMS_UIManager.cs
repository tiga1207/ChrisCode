using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PMS_UIManager : MonoBehaviour
{
    public static PMS_UIManager Instance;

    private void Awake() //½Ì±ÛÅæ »ç¿ë
    {
        InitializeSingleton();
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
