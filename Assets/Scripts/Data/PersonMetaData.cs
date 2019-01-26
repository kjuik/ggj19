using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Person Meta Data")]
    public class PersonMetaData : ScriptableObject
    {
        public Sprite Photo;
        public Sprite DatingCharacter;
        public Color NameTextColor;
    }
}