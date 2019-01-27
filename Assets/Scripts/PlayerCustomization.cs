using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    [SerializeField] private Image playerFace;

    [SerializeField] private Button ContinueButton;
    [SerializeField] private InputField Name;

    void Start()
    {
        playerFace.color = DataManager.Instance.SkinColor;
        Name.text = DataManager.Instance.PlayerName;
        OnNameUpdated();
    }

    public void NextColor()
    {
        playerFace.color = colors[
            (colors.IndexOf(playerFace.color) + 1) % (colors.Count)
        ];
    }

    public void OnNameUpdated()
    {
        ContinueButton.interactable = (Name.text != "");
    }

    public void OnContinue()
    {
        DataManager.Instance.SkinColor = playerFace.color;
        DataManager.Instance.PlayerName = Name.text;
        FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("Home"));
    }
}
