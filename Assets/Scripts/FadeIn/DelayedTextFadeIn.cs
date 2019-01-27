using UnityEngine;
using UnityEngine.UI;

public class DelayedTextFadeIn : DelayedFadeIn
{
    Text text;

    private void Awake() => text = GetComponent<Text>();

    protected override Color Color
    {
        get { return text.color; }
        set { text.color = value; }
    }

}
