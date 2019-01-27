using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Image Photo;
    [SerializeField] private Text Name;
    [SerializeField] private Text Description;
    [SerializeField] private Text Comment;

    [SerializeField] private List<GameObject> DeactivateOnEndGame;
    [SerializeField] private List<GameObject> ActivateOnEndGame;

    [SerializeField] private Image Hand;

    private void Awake()
    {
        Hand.color = DataManager.Instance.SkinColor;
    }

    private void Start()
    {
        if (DataManager.Instance.People.TrueForAll(person => person.Status == PersonStatus.TheftSucceeded))
        {
            EndGame();
        }
        else
        {
            if (DataManager.Instance.ChosenPerson.Status == PersonStatus.TheftSucceeded)
                DataManager.Instance.ChooseNextAvailablePerson();

            Refresh();
        }
    }

    public void Next()
    {
        DataManager.Instance.ChooseNextAvailablePerson();
        if (!Photo.gameObject.activeSelf)
            ToggleInfoOrPhoto();

        Refresh();
    }

    public void Date()
    {
        FadeInOut.Instance.FadeOut(() => 
            SceneManager.LoadScene(DataManager.Instance.ChosenPerson.Status == PersonStatus.DateSucceeded 
            ? "Theft" 
            : "Date"));
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
        Comment.GetComponent<TextScrolling>().Scroll();
    }

    private void EndGame()
    {
        foreach (var go in DeactivateOnEndGame)
            go.SetActive(false);

        foreach (var go in ActivateOnEndGame)
            go.SetActive(true);
    }

    public void GoToTitle() {
        DataManager.Instance.Reset();
        FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("SplashScreen"));
    }
}
