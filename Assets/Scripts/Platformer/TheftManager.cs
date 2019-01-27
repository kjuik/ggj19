using System.Collections;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityUtilities;

namespace Platformer
{
    public class TheftManager : SingletonMonoBehaviour<TheftManager>
    {
        [SerializeField] GameObject overlay;
        [SerializeField] GameObject playerWonNotification;
        [SerializeField] float waitUntilFadeout = 1f;
        [SerializeField] Transform levelContainer;
        [SerializeField] GameObject dateUi;
        [SerializeField] Text characterDialogueOnFail;
        [SerializeField] Text characterName;
        [SerializeField] Image characterImage;

        AudioSource audioSource;

        public bool Running { get; private set; }

        void Awake()
        {
            Running = true;
            overlay.SetActive(false);
            playerWonNotification.SetActive(false);
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            for (var i = 0; i < levelContainer.childCount; i++)
            {
                levelContainer.GetChild(i).gameObject.SetActive(false);
            }
            
            var level = levelContainer.Find(DataManager.Instance.ChosenPerson.Id);
            level.gameObject.SetActive(true);
        }
        
        public void KillPlayer(AudioClip sfx, string gameOverMessage)
        {
            if (!Running)
                return;

            if (sfx != null)
            {
                audioSource.PlayOneShot(sfx);
            }

            var chosenPerson = DataManager.Instance.ChosenPerson;
            chosenPerson.Status = PersonStatus.DateSucceeded;
            
            Time.timeScale = 0;
            Running = false;
            SetUpCharacterReactionOnFail(gameOverMessage, chosenPerson);
        }

        void SetUpCharacterReactionOnFail(string gameOverMessage, Person chosenPerson) {
            dateUi.SetActive(true);
            characterDialogueOnFail.text = gameOverMessage;
            characterName.text = chosenPerson.Name;
            characterDialogueOnFail.color = chosenPerson.MetaData.NameTextColor;
            characterName.color = chosenPerson.MetaData.NameTextColor;
            characterImage.sprite =
                chosenPerson.MetaData.Expressions.First(expression => expression.key == "unhappy").image;
            characterDialogueOnFail.GetComponent<TextScrolling>().Scroll();
            characterDialogueOnFail.GetComponent<TextScrolling>().onScrollingDone += SwitchToHomeAfterDelay;
        }

        public void ReachedExit()
        {
            if (!Running)
                return;
            
            DataManager.Instance.ChosenPerson.Status = PersonStatus.TheftSucceeded;
            
            overlay.SetActive(true);
            playerWonNotification.SetActive(true);
            SwitchToHomeAfterDelay();
        }

        public void SwitchToHomeAfterDelay()
        {
            StartCoroutine(SwitchToHomeAfterDelayCoroutine());
        }

        IEnumerator SwitchToHomeAfterDelayCoroutine()
        {
            yield return new WaitForSecondsRealtime(waitUntilFadeout);
            
            FadeInOut.Instance.FadeOut(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Home");
            });
        }

        public void PlaySfx(AudioClip sfx)
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}