using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMS_UIRegister : MonoBehaviour
{
    //UI요소를 저장할 것을 담아 두는 공간 및 불러올 때 저장공간
    //불러 올 때 key값은 UI이름으로
    private static Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();

    //UI요소들을 Register 하는곳 나중에 UI이름을 통해 Resource에서 들고올 수 있게
    public static void RegisterPrefab(string panelName, GameObject prefab)
    {
        if (!prefabMap.ContainsKey(panelName))
        {
            prefabMap.Add(panelName, prefab);
        }
    }

    //프리팹으로 저장된 UI요소를 가져오기
    public static GameObject GetPrefab(string panelName)
    {
        prefabMap.TryGetValue(panelName, out GameObject prefab);
        return prefab;
    }
}
