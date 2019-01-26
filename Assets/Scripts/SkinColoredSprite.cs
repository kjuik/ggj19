using UnityEngine;

namespace DefaultNamespace
{
    public class SkinColoredSprite : MonoBehaviour
    {
        void Start()
        {
            GetComponent<SpriteRenderer>().color = DataManager.Instance.SkinColor;
        }
    }
}