using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextScrolling : MonoBehaviour
{
    const string startTransparentTag = "<color=#00000000>";
    const string endTransparentTag = "</color>";

    [SerializeField] float secondsPerLetter = 0.025f;

    Text text;

    void Awake() =>
        text = GetComponent<Text>();
        
    public void Scroll()
    {
        StopAllCoroutines();
        StartCoroutine(ScrollCoroutine());
    }

    IEnumerator ScrollCoroutine()
    {
        var fullText = text.text;
        var currentText = "";
        var remainingText = fullText;
        var currentLetterIndex = 0;

        text.text = currentText + startTransparentTag + remainingText + endTransparentTag;

        while (currentText != fullText)
        {
            yield return new WaitForSeconds(secondsPerLetter);

            currentLetterIndex++;
            currentText = fullText.Substring(0, currentLetterIndex);
            remainingText = currentLetterIndex >= fullText.Length ?
                "" : 
                fullText.Substring(currentLetterIndex);

            text.text = currentText + startTransparentTag + remainingText + endTransparentTag;
        }
    }
}
