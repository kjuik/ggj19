using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Image Photo;
    [SerializeField] private Text Name;
    [SerializeField] private Text Description;
    [SerializeField] private Text Comment;

    private void Start() => Refresh();

    public void Next()
    {
        DataManager.Instance.ChooseNextAvailablePerson();
        if (!Photo.gameObject.activeSelf)
            ToggleInfoOrPhoto();

        Refresh();
    }

    public void Date()
    {
        FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("Date"));
    }

    public void ToggleInfoOrPhoto()
    {
        Photo.gameObject.SetActive(!Photo.gameObject.activeSelf);
        Description.gameObject.SetActive(!Description.gameObject.activeSelf);
    }

    private void Refresh()
    {
        Photo.sprite = DataManager.Instance.ChosenPerson.MetaData.Photo;
        Name.text = DataManager.Instance.ChosenPerson.Name;
        Description.text = DataManager.Instance.ChosenPerson.Bio;
        Comment.text = DataManager.Instance.ChosenPerson.FurnitureComment;
    }
}
