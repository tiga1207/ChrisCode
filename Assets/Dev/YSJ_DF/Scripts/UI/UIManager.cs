using UnityEngine;

namespace Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private Canvas _mainCanvas;

        private Transform _hudRoot;
        private Transform _popupRoot;
        private Transform _overlayRoot;

        private int _hudSortingOrder = (int)UILevel.HUD * 100;
        private int _popupSortingOrder = (int)UILevel.Popup * 100;
        private int _overlaySortingOrder = (int)UILevel.Overlay * 100;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeRoots();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeRoots()
        {
            if (_mainCanvas == null)
            {
                _mainCanvas = FindObjectOfType<Canvas>();
                if (_mainCanvas == null)
                {
                    Debug.LogError("[UIManager] Canvas not found in scene.");
                    return;
                }
            }

            _hudRoot = CreateRoot("HUDRoot", _hudSortingOrder);
            _popupRoot = CreateRoot("PopupRoot", _popupSortingOrder);
            _overlayRoot = CreateRoot("OverlayRoot", _overlaySortingOrder);
        }

        private Transform CreateRoot(string name, int sortingOrder)
        {
            Transform root = _mainCanvas.transform.Find(name);

            if (root == null)
            {
                GameObject go = new GameObject(name);
                root = go.transform;
                root.SetParent(_mainCanvas.transform);
                root.localScale = Vector3.one;
                root.localPosition = Vector3.zero;
                root.localRotation = Quaternion.identity;

                var canvas = go.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = sortingOrder;

                go.AddComponent<CanvasGroup>();
            }

            return root;
        }

        // 이후에 이 Root들을 사용해서 패널을 각각 부모로 배치하게 된다.
        public Transform GetRoot(UILevel level)
        {
            return level switch
            {
                UILevel.HUD => _hudRoot,
                UILevel.Popup => _popupRoot,
                UILevel.Overlay => _overlayRoot,
                _ => _hudRoot
            };
        }

        public void RegisterPanelCanvas(UIPanelBase panel)
        {
            Canvas panelCanvas = panel.GetComponent<Canvas>();

            if (panelCanvas == null)
            {
                panelCanvas = panel.gameObject.AddComponent<Canvas>();
                panel.gameObject.AddComponent<CanvasGroup>();
            }

            panelCanvas.overrideSorting = true;

            if (panel.Level == UILevel.Popup)
            {
                panelCanvas.sortingOrder = ++_popupSortingOrder;
            }
            else if (panel.Level == UILevel.Overlay)
            {
                panelCanvas.sortingOrder = ++_overlaySortingOrder;
            }
            else
            {
                panelCanvas.sortingOrder = 0; // HUD는 기본 Canvas에 포함되므로 따로 순서 조정 안 함
            }
        }
    }
}
