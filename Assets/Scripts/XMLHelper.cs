using System.Xml.Linq;

namespace DefaultNamespace
{
    public static class XMLHelper
    {
        public static string GetChildValue(this XElement xElement, string childName)
        {
            return xElement.Element(XName.Get(childName)).Value;
        }
    }
}