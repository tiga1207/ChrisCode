using UnityEngine;

namespace Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanelBase : MonoBehaviour
    {
        public abstract UILevel Level { get; }

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;

        protected virtual void Start()
        {
            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            _canvas = GetComponent<Canvas>();
            if (_canvas == null)
            {
                _canvas = gameObject.AddComponent<Canvas>();
            }

            _canvas.overrideSorting = true;

            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            if (UIManager.Instance != null)
            {
                UIManager.Instance.RegisterPanelCanvas(this);
            }
            else
            {
                Debug.LogWarning("[UIPanelBase] UIManager.Instance가 존재하지 않아 Canvas SortingOrder를 설정하지 못했습니다.");
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }
    }
}