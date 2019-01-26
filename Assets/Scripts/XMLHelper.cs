using System.Xml.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public static class XMLHelper
    {
        public static string GetChildValue(this XElement xElement, string childName)
        {
            var child = xElement.Element(XName.Get(childName));
            if (child == null)
            {
                Debug.LogError("Child not found: " + childName + " in " + xElement);
                return "";
            }
            
            return child.Value;
        }
    }
}