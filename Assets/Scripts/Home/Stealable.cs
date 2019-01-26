using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Stealable : MonoBehaviour
{
    [SerializeField] ScriptableObject characterDataObject;

    private void Start()
    {
        GetComponent<Image>().enabled = 
            DataManager.Instance.People
            .Where(p => p.Id == characterDataObject.name).First()
            .Status == PersonStatus.Succeeded;
    }
}
