using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Manager
{
    // 전체적인 비동기 씬로드 방식 사용
    // 그냥 씬로드 방식 사용 안함
    public class SceneManagerEx : SimpleSingleton<SceneManagerEx>
    {
        // 씬이 로딩이 끝났을 때, 즉 이동하려는 씬이 로드 다 되었을 때 실행됨.
        // 자신이 원하는 씬이 로도가 되었을 때, 필요하다고 생각하는 것들을 넣어주면된다.
        // 람다식으로 달아도 되고 등등
        // 사용하다고 하면 Title에서 인게임 넘어갈 때 사용할 가능성 있음(로딩 UI 닫거나, bgm 틀어주거나..., 데이터 로드 담당해도되고)
        public event Action<string> OnSceneLoaded;

        [Header("Fade Settings(공용 사용)")]
        [SerializeField] private CanvasGroup _fadeCanvasGroup; // DontDestroy Object
        [SerializeField] private float _fadeDuration = 1f;

        [Header("Loading UI(공용 사용)")]
        [SerializeField] private GameObject _loadingUI; // DontDestroy Object
        [SerializeField] private Slider _loadingBar;

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        // Scene
        public void LoadSceneAsync(string sceneName)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }
        public void LoadSceneWithFade(string sceneName)
        {
            StartCoroutine(LoadSceneWithFadeRoutine(sceneName));
        }
        public void LoadSceneAsyncWithLoading(string sceneName)
        {
            StartCoroutine(LoadSceneWithLoadingUI(sceneName));
        }
        public void UnloadSceneAsync(string sceneName)
        {
            // 나중에 씬을 여러개 띄어주고 해제할 때 사용할 예정(지금 사용안함)
            StartCoroutine(UnloadSceneRoutine(sceneName));
        }

        // Coroutine
        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            // 로딩 진행 상태와 완료 여부 객체(비동기)
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); // 비동기 씬로드
            yield return new WaitUntil(() => op.isDone); // 로딩 완료 여부 확인까지 기다리기
            OnSceneLoaded?.Invoke(sceneName); // 
        }
        private IEnumerator LoadSceneWithFadeRoutine(string sceneName)
        {
            yield return StartCoroutine(FadeOut());
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); // 비동기 씬로드
            yield return new WaitUntil(() => op.isDone); // 로딩 완료 여부 확인까지 기다리기
            OnSceneLoaded?.Invoke(sceneName);
            yield return StartCoroutine(FadeIn());
        }
        private IEnumerator LoadSceneWithLoadingUI(string sceneName)
        {
            if (_loadingUI != null)
                _loadingUI.SetActive(true);

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); // 비동기
            op.allowSceneActivation = false; // 자동 씬 전환 안되도록

            // 90%까지 대기
            while (op.progress < 0.9f)
            {
                if (_loadingBar != null)
                    _loadingBar.value = op.progress;
                yield return null;
            }

            if (_loadingBar != null)
                _loadingBar.value = 1f;

            yield return new WaitForSeconds(0.5f);
            op.allowSceneActivation = true; // 씬전환 가능

            yield return new WaitUntil(() => op.isDone); // 완료

            if (_loadingUI != null)
                _loadingUI.SetActive(false);

            OnSceneLoaded?.Invoke(sceneName);
        }
        private IEnumerator UnloadSceneRoutine(string sceneName)
        {
            AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);
            yield return new WaitUntil(() => op.isDone);
        }

        // ETC(나중에 옮길 예정)
        private IEnumerator FadeOut()
        {
            if (_fadeCanvasGroup == null) yield break;
            _fadeCanvasGroup.blocksRaycasts = true;
            float t = 0f;
            while (t < _fadeDuration)
            {
                t += Time.deltaTime;
                _fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, t / _fadeDuration);
                yield return null;
            }
        }
        private IEnumerator FadeIn()
        {
            if (_fadeCanvasGroup == null) yield break;
            float t = 0f;
            while (t < _fadeDuration)
            {
                t += Time.deltaTime;
                _fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, t / _fadeDuration);
                yield return null;
            }
            _fadeCanvasGroup.blocksRaycasts = false;
        }
    }
}
