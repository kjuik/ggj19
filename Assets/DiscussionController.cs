using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiscussionController : MonoBehaviour {
    public Text nameText;
    public Image character;
    public Text dialogueText;
    public List<Button> answers;
    public Button next;
    public Button goHome;
    public Text lineBeforeTheft;
    [SerializeField] string lineBeforeTheftText;
    int currentDialogueNode;
    
    Person ChosenPerson => DataManager.Instance.ChosenPerson;
    
    void Start() {
        goHome.gameObject.SetActive(false);
        lineBeforeTheft.gameObject.SetActive(false);
        nameText.text = ChosenPerson.Name;
        character.sprite = ChosenPerson.MetaData.DatingCharacter;
        UpdateDialogue();
    }

    public void Next() 
    {
        currentDialogueNode++;

        if (currentDialogueNode < ChosenPerson.Dialogue.Count)
            UpdateDialogue();
        else
            FadeInOut.Instance.FadeOut(ShowLineBeforeTheft);
    }

    void UpdateDialogue() {
        switch (ChosenPerson.Dialogue[currentDialogueNode].Type) {
            case DialogueBlockType.Line:
                ShowLine((DialogueLine) ChosenPerson.Dialogue[currentDialogueNode]);
                break;
            case DialogueBlockType.Question:
                ShowQuestion((DialogueQuestion)ChosenPerson.Dialogue[currentDialogueNode]);
                break; 
        }
    }

    void ShowLine(DialogueLine dialogueLine) {
        next.gameObject.SetActive(true);
        foreach (var answer in answers) 
            answer.gameObject.SetActive(false);

        UpdateLineText(dialogueLine);
    }

    void UpdateLineText(DialogueLine dialogueLine) {
        nameText.text = dialogueLine.Speaker == Speaker.They ? ChosenPerson.Name : "You";
        dialogueText.text = dialogueLine.Text;
    }

    void ShowQuestion(DialogueQuestion dialogueQuestion) {
        next.gameObject.SetActive(false);

        for (var i = 0; i < answers.Count; i++) {
            answers[i].gameObject.SetActive(true);
            answers[i].GetComponentInChildren<Text>().text = dialogueQuestion.Answers[i].YourAnswerText;
            var answer = dialogueQuestion.Answers[i];
            answers[i].onClick.AddListener(() => OnAnswer(answer));
        }

        UpdateLineText(dialogueQuestion.QuestionLine);
    }

    void OnAnswer(DialogueAnswer dialogueQuestionAnswer) {
        foreach (var answer in answers) 
            answer.onClick.RemoveAllListeners();
        ShowLine(dialogueQuestionAnswer.TheirReactionLine);
        if (dialogueQuestionAnswer.Correct)
            next.gameObject.SetActive(true);
        else {
            ChosenPerson.Status = PersonStatus.Failed;
            goHome.gameObject.SetActive(true);
        }
    }
    
    public void GoHome()
    {
        FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("Home"));
    }

    public void GoToTheft() {
        SceneManager.LoadScene("Theft");
    }
    
    void ShowLineBeforeTheft() {
        lineBeforeTheft.text = lineBeforeTheftText;
        lineBeforeTheft.gameObject.SetActive(true);
    }
}
