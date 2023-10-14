using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game.Controller.Restart.TouchDetection
{
    public class RestartTouchDetection : MonoBehaviour
    {
        [SerializeField]
        private GameController gameController;

        [Header("Text")]
        [SerializeField]
        private TMP_Text textRestart;

        private bool checkForTouch = false;
        private bool restartStarted = false;

        private void Awake()
        {
            gameController.GameOver += StartTouchCheck;
            textRestart.DOFade(0f, 0f);
        }

        private void OnDestroy()
        {
            gameController.GameOver -= StartTouchCheck;
        }

        private void Update()
        {
            if (checkForTouch)
            {
                RestartStart();
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        checkForTouch = false;
                        StartCoroutine(RestartEnd());
                    }
                }
            }
        }

        private void StartTouchCheck()
        {
            checkForTouch = true;
        }

        private void RestartStart()
        {
            if (!restartStarted)
            {
                restartStarted = true;

                textRestart.DOFade(1f, 0.5f);
                StartCoroutine(ChangeTimeScaleSmoothly(0f, 1f));
            }
        }

        private IEnumerator RestartEnd()
        {
            textRestart.DOFade(0f, 0.75f).SetUpdate(true);
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private IEnumerator ChangeTimeScaleSmoothly(float target, float duration)
        {
            float start = Time.timeScale;
            float currentTime = 0f;

            while (currentTime < duration)
            {
                float t = currentTime / duration;
                Time.timeScale = Mathf.Lerp(start, target, t);
                currentTime += Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = target;
        }
    }
}