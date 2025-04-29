using UnityEngine;

namespace Scripts.Interface
{
    public interface IManager
    {
        void Initialize();
        void Cleanup();
        GameObject GetGameObject();

        void UpdateManager();
    }
}

