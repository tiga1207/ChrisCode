using Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ManagerGroup : MonoBehaviour
    {
        private static ManagerGroup _instance;
        public static ManagerGroup Instance
        {
            get
            {
                if (_instance == null)
                {
                    string groupName = "@ManagerGroup";
                    GameObject go = GameObject.Find(groupName);
                    if (go == null)
                    {
                        go = new GameObject("@ManagerGroup");
                        DontDestroyOnLoad(go);
                    }

                    _instance = go.GetOrAddComponent<ManagerGroup>();
                }

                return _instance;
            }
        }

        private bool _isManagersInit = false;

        private List<IManager> _managers = new List<IManager>();

        public void RegisterManager(IManager manager)
        {
            if (!_managers.Contains(manager))
            {
                _managers.Add(manager);
                var go = manager.GetGameObject();
                go.transform.parent = this.transform;
            }
        }

        public void InitializeManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Initialize();
            }

            _isManagersInit = true;
        }

        public void CleanupManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Cleanup();
            }

            _isManagersInit = false;
        }

        private void Update()
        {
            
        }
    }
}
