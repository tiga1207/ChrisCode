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

        private List<IManager> _managers = new List<IManager>();

        // Register
        public void RegisterManager(IManager manager)
        {
            GameObject go = manager.GetGameObject();

            _managers.Add(manager);
            go.transform.parent = this.transform;
        }
        public void RegisterManager(GameObject manager)
        {
            IManager cmp = manager.GetComponent<IManager>();

            if (cmp != null)
                RegisterManager(cmp);
        }

        public void RegisterManager(params IManager[] manager)
        {
            foreach (IManager go in manager)
                RegisterManager(go);
        }
        public void RegisterManager(params GameObject[] manager)
        {
            foreach (GameObject go in manager)
                RegisterManager(go);
        }


        public void InitializeManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Initialize();
                manager.GetGameObject().transform.parent = this.transform;
            }
        }
        public void CleanupManagers()
        {
            foreach (var manager in _managers)
                manager.Cleanup();
        }
    }
}
