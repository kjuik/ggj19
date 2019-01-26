using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Person Meta Data")]
    public class PersonMetaData : ScriptableObject
    {
        public Sprite Photo;
        public Expression[] Expressions;
        public Color NameTextColor;
        public Sprite StealableCarryingSprite;
    }

    [Serializable]
    public class Expression {
        public Sprite image;
        public string key;
    }
}