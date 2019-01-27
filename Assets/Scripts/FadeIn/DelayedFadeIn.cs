using System.Collections;
using UnityEngine;

public abstract class DelayedFadeIn : MonoBehaviour
{
    [SerializeField] float delaySeconds = 1f;
    [SerializeField] float fadeTimeSeconds = 1f;

    IEnumerator Start()
    {
        var startColor = new Color(Color.r, Color.g, Color.b, 0f);
        var endColor = new Color(Color.r, Color.g, Color.b, Color.a);

        Color = startColor;

        yield return new WaitForSeconds(delaySeconds);

        var startTime = Time.time;
        while (Time.time < startTime + fadeTimeSeconds)
        {
            Color = Color.Lerp(startColor, endColor, (Time.time - startTime) / fadeTimeSeconds);
            yield return 0;
        }

        Color = endColor;
    }

    protected abstract Color Color { get; set; }
}
