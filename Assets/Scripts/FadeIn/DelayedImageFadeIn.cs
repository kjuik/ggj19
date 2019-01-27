using UnityEngine;
using UnityEngine.UI;

public class DelayedImageFadeIn : DelayedFadeIn
{
    Image image;

    private void Awake() => image = GetComponent<Image>();

    protected override Color Color
    {
        get { return image.color; }
        set { image.color = value; }
    }

}
