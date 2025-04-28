using UnityEngine;

namespace Scripts.Scene
{
    // 씬을 로드하고 제일 먼저 찾는 오브젝트에 넣어줄 .cs
    public abstract class SceneBase : MonoBehaviour
    {
        // 해당 씬이 어떤 씬인지
        public abstract SceneType SceneType { get; }

        // 리소스 로드 매니저 개발시 삭제 예정

        [SerializeField] protected GameObject _sceneUIPrefab;

        protected virtual void Start()
        {
            InitializeSceneUI();
        }

        protected abstract void InitializeSceneUI();
    }
}