using Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PMS_UIManager : MonoBehaviour
{
    public static PMS_UIManager Instance { get; private set; }  //싱글톤 객체생성
    [SerializeField] private Transform _uiRoot;     //ui의 부모가 되는 요소
    private SceneType _currentMainUISceneType = SceneType.None; //현재 어떤 UI를 뛰울 씬타입인지 //나중에 아마 수정 SceneManager에서 바뀔 때 어떤 UI요소를 넣으면될지

    private readonly Dictionary<string, UIPanelBase> _panelInstances = new(); //패널들 읽기전용으로 ?
    private readonly Stack<UIPanelBase> _panelStack = new(); // 패널 스택(저장공간)
    private readonly List<string> _savedPanelStack = new(); // 저장된 패널 스택(이름)

    private void Awake() //싱글톤 사용
    {
        InitializeSingleton();
    }

    //?
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        CloseAllPanels();
    }

    //씬타입에 맞게 UI요소를 들고오는 방식
    public void LoadSceneUI(SceneType sceneType, GameObject uiScenePrefab = null)
    {
        //씬에 이름을 따와서
        string sceneStr = sceneType.ToString();

        GameObject sceneObject;
        //씬에 맞는 Ui요소들을 Resources를 통해 들고오는것 
        if (uiScenePrefab == null)
            sceneObject = Resources.Load<GameObject>($"UI/UI_Scene_{sceneStr}");
        else
            sceneObject = uiScenePrefab;    //else구문은 잘모르겟다.

        //해당 UI객체 생성
        Instantiate(sceneObject);
    }

    //해당 UI요소를 여는것
    public void OpenPanel(string panelName)
    {
        //UI의 이름을 키값으로 통해 들고온다, 해당 Ui오브젝트가 있으면 들고와서 쓴다
        if (_panelInstances.ContainsKey(panelName))
        {
            //이미 만들어진 패널이면 새로 만들지 않고 스택에 쌓기만 함
            PushPanel(_panelInstances[panelName]);
            return;
        }

        //UIRegister에서 해당
        var panelPrefab = UIRegistry.GetPrefab(panelName);
        if (panelPrefab == null)
        {
            Debug.LogError($"[UIManager] Cannot find prefab for {panelName}");
            return;
        }

        var panelInstance = Instantiate(panelPrefab, _uiRoot);
        var panelBase = panelInstance.GetComponent<UIPanelBase>();
        _panelInstances.Add(panelName, panelBase);

        PushPanel(panelBase);
    }

    public void CloseCurrentPanel()
    {
        if (_panelStack.Count == 0)
            return;

        var topPanel = _panelStack.Pop();
        topPanel.Hide();
        Destroy(topPanel.gameObject, 1f);

        if (_panelStack.Count > 0)
        {
            var previousPanel = _panelStack.Peek();
            previousPanel.Show();
        }
    }

    public void CloseAllPanels()
    {
        while (_panelStack.Count > 0)
        {
            var panel = _panelStack.Pop();
            if (panel != null)
            {
                panel.Hide();
                Destroy(panel.gameObject, 1f);
            }
        }

        _panelInstances.Clear();
    }

    public bool HasPanels()
    {
        return _panelStack.Count > 0;
    }

    public void SavePanelStack()
    {
        _savedPanelStack.Clear();

        foreach (var panel in _panelStack)
        {
            _savedPanelStack.Add(panel.name.Replace("(Clone)", "").Trim());
        }

        _savedPanelStack.Reverse();
        Debug.Log("[UIManager] Panel Stack Saved");
    }

    public void RestorePanelStack()
    {
        if (_savedPanelStack.Count == 0)
            return;

        CloseAllPanels();

        foreach (var panelName in _savedPanelStack)
        {
            OpenPanel(panelName);
        }

        Debug.Log("[UIManager] Panel Stack Restored");
    }

    //stack형 ui사용 
    private void PushPanel(UIPanelBase newPanel)
    {
        if (_panelStack.Count > 0)
        {
            var current = _panelStack.Peek();
            current.Hide();
        }

        _panelStack.Push(newPanel);
        newPanel.Show();
    }
}
