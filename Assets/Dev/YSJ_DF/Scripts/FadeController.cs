using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Scripts
{
    public class FadeController : MonoBehaviour
    {
        public static FadeController Instance { get; private set; }

        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 1.0f;

        private void Awake()
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

        public IEnumerator FadeOut()
        {
            float timer = 0f;
            Color color = fadeImage.color;
            while (timer <= fadeDuration)
            {
                color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
                fadeImage.color = color;
                timer += Time.deltaTime;
                yield return null;
            }
            color.a = 1;
            fadeImage.color = color;
        }

        public IEnumerator FadeIn()
        {
            float timer = 0f;
            Color color = fadeImage.color;
            while (timer <= fadeDuration)
            {
                color.a = Mathf.Lerp(1, 0, timer / fadeDuration);
                fadeImage.color = color;
                timer += Time.deltaTime;
                yield return null;
            }
            color.a = 0;
            fadeImage.color = color;
        }
    }

}