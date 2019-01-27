using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
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
    bool showAnswersNext;
    bool updateReactionLine;
    DialogueAnswer currentAnswer;
    
    Person ChosenPerson => DataManager.Instance.ChosenPerson;
    
    void Start() {
        goHome.gameObject.SetActive(false);
        lineBeforeTheft.gameObject.SetActive(false);
        nameText.text = ChosenPerson.Name;
        GoToNewDialogue();
    }

    public void Next() 
    {
        IfNotScrolling(UpdateDialogue);
    }

    void IfNotScrolling(Action action) {
        var scroller = dialogueText.GetComponent<TextScrolling>();
        if (scroller.isScrolling)
            scroller.SkipScrolling();
        else {
            action();
        }
    }

    void UpdateDialogue() {
        if (showAnswersNext)
            ShowAnswers((DialogueQuestion)ChosenPerson.Dialogue[currentDialogueNode]);
        else if (updateReactionLine) {
            if (currentAnswer.ReactionLinesOngoing)
                UpdateReactionLine();
        } else {
            currentDialogueNode++;
            if (currentDialogueNode < ChosenPerson.Dialogue.Count)
                GoToNewDialogue();
            else
                FadeInOut.Instance.FadeOut(ShowLineBeforeTheft);
        }
    }

    void UpdateReactionLine() {
        ShowLine(currentAnswer.NextReactionLine());
        if (!currentAnswer.ReactionLinesOngoing) {
            if (!currentAnswer.Correct)
                SetUpGoHome();
            updateReactionLine = false;
        }
    }

    void GoToNewDialogue() {
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

    void ShowQuestion(DialogueQuestion dialogueQuestion) {
        ShowLine(dialogueQuestion.QuestionLine);
        showAnswersNext = true;
    }

    void UpdateLineText(DialogueLine dialogueLine) {
        if (dialogueLine.Speaker == Speaker.They) {
            nameText.text = ChosenPerson.Name;
            nameText.color = ChosenPerson.MetaData.NameTextColor;
        } else {
            nameText.text = "You";
            nameText.color = Color.black;
        }

        character.sprite = ChosenPerson.MetaData.Expressions
                                       .First(expression => expression.key == dialogueLine.Expression).image;
        
        dialogueText.text = dialogueLine.Text;
        dialogueText.GetComponent<TextScrolling>().Scroll();
    }

    void ShowAnswers(DialogueQuestion dialogueQuestion) {
        showAnswersNext = false;
        next.gameObject.SetActive(false);

        for (var i = 0; i < answers.Count; i++) {
            answers[i].gameObject.SetActive(true);
            answers[i].GetComponentInChildren<Text>().text = dialogueQuestion.Answers[i].YourAnswerText;
            var answer = dialogueQuestion.Answers[i];
            answers[i].onClick.AddListener(() => OnAnswer(answer));
        }
    }

    void OnAnswer(DialogueAnswer dialogueQuestionAnswer) {
        foreach (var answer in answers) 
            answer.onClick.RemoveAllListeners();

        updateReactionLine = true;
        currentAnswer = dialogueQuestionAnswer;
        UpdateDialogue();
    }

    void SetUpGoHome() {
        next.gameObject.SetActive(false);
        ChosenPerson.Status = PersonStatus.Failed;
        goHome.gameObject.SetActive(true);
    }
    
    public void GoHome()
    {
        IfNotScrolling(() => FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("Home")));
    }

    void GoToTheft() {
        SceneManager.LoadScene("Theft");
    }
    
    void ShowLineBeforeTheft() {
        lineBeforeTheft.gameObject.SetActive(true);
        lineBeforeTheft.text = lineBeforeTheftText + '\n' + '\n' + ChosenPerson.TheftComment;
        lineBeforeTheft.GetComponent<TextScrolling>().Scroll();
        lineBeforeTheft.GetComponent<TextScrolling>().onScrollingDone += () => StartCoroutine(TriggerGoToTheft());
    }

    IEnumerator TriggerGoToTheft() {
        yield return new WaitForSeconds(3);
        GoToTheft();
    }
}
