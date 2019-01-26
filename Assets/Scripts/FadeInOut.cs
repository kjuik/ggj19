using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityUtilities;

public class FadeInOut : PersistentSingletonMonoBehaviour<FadeInOut>
{
    [SerializeField] Image overlay;

    [SerializeField] float fadeTimeSeconds = 1f;
    [SerializeField] Color color;

    bool isFadedOut = true;

    protected override void OnPersistentSingletonAwake()
    {
        overlay.color = new Color(color.r, color.g, color.b, isFadedOut ? 1f : 0f);
        if (isFadedOut)
            FadeIn();
    }

    protected override void OnSceneSwitched()
    {
        if (isFadedOut)
            FadeIn();
    }

    public void FadeIn(Action onFinished = null)
    {
        isFadedOut = false;
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f, onFinished));
    }

    public void FadeOut(Action onFinished = null)
    {
        isFadedOut = true;
        StopAllCoroutines();
        StartCoroutine(Fade(0f, 1f, onFinished));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, Action onFinished = null)
    {
        var startColor = new Color(color.r, color.g, color.b, startAlpha);
        var endColor = new Color(color.r, color.g, color.b, endAlpha);

        var startTime = Time.time;
        while (Time.time < startTime + fadeTimeSeconds)
        {
            overlay.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / fadeTimeSeconds);
            yield return 0;
        }

        overlay.color = endColor;

        onFinished?.Invoke();
    }
}
