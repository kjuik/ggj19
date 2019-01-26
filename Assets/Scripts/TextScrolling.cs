using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextScrolling : MonoBehaviour
{
    [SerializeField] float secondsPerLetter = 0.025f;

    Text text;

    string fullText;
    string currentText;
    int nextLetterIndex;

    void Awake() =>
        text = GetComponent<Text>();
        
    public void Scroll()
    {
        StopAllCoroutines();
        StartCoroutine(ScrollCoroutine());
    }

    IEnumerator ScrollCoroutine()
    {
        fullText = text.text;
        string currentText = "";
        nextLetterIndex = 0;

        text.text = currentText;

        while (currentText != fullText)
        {
            yield return new WaitForSeconds(secondsPerLetter);

            currentText += fullText[nextLetterIndex];
            nextLetterIndex++;

            text.text = currentText;
        }
    }
}
